using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {

        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()));
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDto loginDto)
        {
            if (authService.Login(loginDto))
            {
                FormsAuthentication.SetAuthCookie(loginDto.AdminMail, false);
                Session["AdminMail"] = loginDto.AdminMail;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return View();
            }
            //Context context = new Context(); // Kurumsal mimari yapisina dönüstürülecek (ödev)
            //var adminUser = context.Admins.FirstOrDefault(x =>
            //    x.AdminMail == admin.AdminMail && x.AdminPassword == admin.AdminPassword);

            //if (adminUser != null)
            //{
            //    FormsAuthentication.SetAuthCookie(adminUser.AdminUserName, false); //Sisteme giriş yapan kişinin form bilgileri buradan alınır. Buradaki false değeri ise, kalıcı bir cookie değerinin olmayacağını belirtir.

            //    Session["AdminMail"] = adminUser.AdminMail; //Oturum yönetimi kodu yazılır. Session içerisinde yazılacak değer; köşeli parantez içerisine(sisteme giriş yapan kullanıcının mail adresi gerekli) yazılır
            //    return RedirectToAction("Index", "AdminCategory");
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
            //return View();
        }
    }
}
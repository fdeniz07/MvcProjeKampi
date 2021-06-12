using System.Linq;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            Context context = new Context(); // Kurumsal mimari yapisina dönüstürülecek (ödev)
            var adminUser = context.Admins.FirstOrDefault(x =>
                x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);

            if (adminUser!=null)
            {
                FormsAuthentication.SetAuthCookie(adminUser.AdminUserName,false); //Sisteme giriş yapan kişinin form bilgileri buradan alınır. Buradaki false değeri ise, kalıcı bir cookie değerinin olmayacağını belirtir.

                Session["AdminUserName"] = adminUser.AdminUserName; //Oturum yönetimi kodu yazılır. Session içerisinde yazılacak değer; köşeli parantez içerisine(sisteme giriş yapan kullanıcının mail adresi gerekli) yazılır
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
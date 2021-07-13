using System.Linq;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;
using MvcProjeKampi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous] //Proje bazinda olusturulan kurallardan muaf tutuyor. Sadece bulundugu sayfa.
    public class LoginController : Controller
    {
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));

        //Context context = new Context();

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogIn(AdminLogInDto adminLogInDto)
        {
            var response = Request["g-recaptcha-response"];
            /* const string secret = "6Lc9zzgbAAAAAFBGD3Gb201yvNAX4Tb5LAzlqy0d";*/ //Localhost icin Google Captcha v2


            /*const string secret = "6LewJJEbAAAAAIslbgvowPTE0lZ8Yiwk5-cV6p7s"; *///Localhost icin Google Captcha v3
            const string secret = "6LdF9ZAbAAAAAOq5NKMx8jK2K-O5ISBLXWOwOKI7"; //"6LdF9ZAbAAAAAOq5NKMx8jK2K-O5ISBLXWOwOKI7"; - //Canli Site icin mail ile site adi kayit yaptirilir Google Captcha v3

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);
            
            if (authService.AdminLogIn(adminLogInDto) && captchaResponse.Success )
            {
                FormsAuthentication.SetAuthCookie(adminLogInDto.AdminMail, false);
                Session["AdminMail"] = adminLogInDto.AdminMail;
                var session = Session["AdminMail"];
                //var adminIdInfo = context.Admins.Where(x => x.AdminMail == session).Select(y => y.AdminId).FirstOrDefault();
                //ViewBag.logIn = adminIdInfo;
               ViewBag.a = session;
                return RedirectToAction("Index", "Statistic");
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return View();
            }
            #region Eski Kodlar
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
            #endregion
        }

        public ActionResult AdminLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("AdminLogin", "LogIn");
        }

        [HttpGet]
        public ActionResult WriterLogIn()
        {
            return View();
        }

        #region Eski Kodlar
        //[HttpPost]
        //public ActionResult WriterLogIn(Writer writer)
        //{
        //    Context context = new Context(); // Kurumsal mimari yapisina dönüstürülecek (ödev)
        //    var writerUser = context.Writers.FirstOrDefault(x =>
        //        x.WriterMail == writer.WriterMail && x.WriterPasswordSalt == writer.WriterPasswordSalt);

        //    if (writerUser != null)
        //    {
        //        FormsAuthentication.SetAuthCookie(writerUser.WriterMail.ToString(), false); //Sisteme giriş yapan yazarin form bilgileri buradan alınır.                                                                                                         Buradaki false değeri ise, kalıcı bir cookie değerinin                                                                                                             olmayacağını belirtir.

        //        Session["WriterMail"] = writerUser.WriterMail; //Oturum yönetimi kodu yazılır. Session içerisinde yazılacak değer; köşeli parantez içerisine(sisteme giriş                                                          yapan kullanıcının mail adresi gerekli) yazılır
        //        return RedirectToAction("MyContent", "WriterPanelContent");
        //    }
        //    else
        //    {
        //        return RedirectToAction("WriterLogin");
        //    }
        //}
        #endregion

        [HttpPost]
        public ActionResult WriterLogIn(WriterLogInDto writerLogInDto)
        {
            var response = Request["g-recaptcha-response"];
            /* const string secret = "6Lc9zzgbAAAAAFBGD3Gb201yvNAX4Tb5LAzlqy0d";*/ //Localhost icin Google Captcha v2


            /*const string secret = "6LewJJEbAAAAAIslbgvowPTE0lZ8Yiwk5-cV6p7s"; *///Localhost icin Google Captcha v3
            const string secret = "6LdF9ZAbAAAAAOq5NKMx8jK2K-O5ISBLXWOwOKI7"; //"6LdF9ZAbAAAAAOq5NKMx8jK2K-O5ISBLXWOwOKI7"; - //Canli Site icin mail ile site adi kayit yaptirilir Google Captcha v3
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);

            if (authService.WriterLogIn(writerLogInDto) && captchaResponse.Success)
            {
                FormsAuthentication.SetAuthCookie(writerLogInDto.WriterMail, false);
                Session["WriterMail"] = writerLogInDto.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return RedirectToAction("WriterLogin");
            }
        }

        public ActionResult WriterLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            //return RedirectToAction("Headings", "Default");
            return RedirectToAction("WriterLogin", "LogIn");
        }
    }
}
using BusinessLayer.Abstract;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {

        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));

        //[HttpGet]
        //public ActionResult AdminRegister()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AdminRegister(AdminLogInDto adminLogInDto)
        //{
        //    authService.AdminRegister(adminLogInDto.AdminUserName, adminLogInDto.AdminMail, adminLogInDto.AdminPassword);
        //    return RedirectToAction("Index", "AdminCategory");
        //}


        [HttpGet]
        public ActionResult WriterRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterRegister(WriterLogInDto writerLogInDto)
        {
            //DB ye kaydetme sirasi cok önemli, aksi durumda yanlis alanlara yanlis veri kaydoluyor
            authService.WriterRegister(
                writerLogInDto.WriterName,
                writerLogInDto.WriterSurName,
                writerLogInDto.WriterTitle,
                writerLogInDto.WriterAbout,
                writerLogInDto.WriterImage,
                writerLogInDto.WriterUserName,
                writerLogInDto.WriterMail,
                writerLogInDto.WriterPassword,
                writerLogInDto.WriterStatus = true
                );
            return RedirectToAction("MyContent", "WriterPanelContent");
        }
    }
}


//string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage
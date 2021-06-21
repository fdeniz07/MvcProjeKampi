using BusinessLayer.Abstract;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;

namespace MvcProjeKampi.Controllers
{
    public class RegisterController : Controller
    {

        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()),new WriterManager(new EfWriterDal()));

        [HttpGet]
        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminRegister(AdminLogInDto adminLogInDto)
        {
            authService.AdminRegister(adminLogInDto.AdminUserName,adminLogInDto.AdminMail, adminLogInDto.AdminPassword);
            return RedirectToAction("Index", "AdminCategory");
        }


        [HttpGet]
        public ActionResult WriterRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WriterRegister(WriterLogInDto writerLogInDto)
        {
            authService.WriterRegister(writerLogInDto.WriterUserName, writerLogInDto.WriterMail, writerLogInDto.WriterPassword);
            return RedirectToAction("MyContent", "WriterPanelContent");
        }
    }
}
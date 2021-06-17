using BusinessLayer.Abstract;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dto;

namespace MvcProjeKampi.Controllers
{
    public class RegisterController : Controller
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
            authService.Register(loginDto.AdminUserName,loginDto.AdminMail, loginDto.AdminPassword);
            return RedirectToAction("Index", "AdminCategory");
        }
    }
}
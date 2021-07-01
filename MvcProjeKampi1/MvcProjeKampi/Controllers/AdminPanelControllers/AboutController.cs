using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        public ActionResult Index()
        {
            var aboutValues = aboutManager.GetList();
            return View(aboutValues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            aboutManager.AboutAdd(about);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        public ActionResult IsActive(int id)
        {
            var aboutValue = aboutManager.GetByIdAbout(id);
            if (aboutValue.Status)
            {
                aboutValue.Status = false;
            }
            else
            {
                aboutValue.Status = true;
            }
            aboutManager.AboutUpdate(aboutValue);
            return RedirectToAction("Index");
        }
    }
}

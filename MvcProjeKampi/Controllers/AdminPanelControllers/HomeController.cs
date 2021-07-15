using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using System.Linq;

namespace MvcProjeKampi.Controllers
{
    public class HomeController : Controller
    {
        ScreenShotManager screenShotManager = new ScreenShotManager(new EfScreenShotDal());
        Context _context = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult HomePage()
        {
            var files = screenShotManager.GetList();

            var totalCategory = _context.Categories.Count(); //Toplam Kategori Sayisi
            ViewBag.totalNumberOfCategories = totalCategory;

            var totalHeading = _context.Headings.Count(); //Toplam Baslik sayisi
            ViewBag.totalHeading = totalHeading;

            var totalWriter = _context.Writers.Count();//Toplam Yazar sayisi
            ViewBag.totalWriter = totalWriter;

            var categoryStatusTrue = _context.Categories.Count(x => x.StatusId == 2); // Kategoriler tablosundaki aktif kategori sayisi
            ViewBag.activeCategories = categoryStatusTrue;

            return View(files);
        }
    }
}
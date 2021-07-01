using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        ContentManager contentManager = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllContent()
        {
            var values = contentManager.GetList();

            return View(values);
        }

        [HttpPost]
        public ActionResult GetAllContent(string searchKeyWord)
        {
            var values = contentManager.GetListBySearch(searchKeyWord);

            return View(values);
        }

        public ActionResult ContentByHeading(int id)
        {
            var contentValues = contentManager.GetListByHeadingId(id);
            return View(contentValues);
        }
    }
}
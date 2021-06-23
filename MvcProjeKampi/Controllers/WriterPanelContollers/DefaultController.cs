using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers.WriterPanelContollers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        ContentManager contentManager = new ContentManager(new EfContentDal());

        public ActionResult Headings()
        {
            var headingList = headingManager.GetList();
            return View(headingList);
        }

        public PartialViewResult Index(int id=0)
        {
            var contentList = contentManager.GetListByHeadingId(id);
            return PartialView(contentList);
        }
    }
}
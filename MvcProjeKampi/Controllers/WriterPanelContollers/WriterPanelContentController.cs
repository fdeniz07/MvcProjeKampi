using BusinessLayer.Concrete;
using System.Web.Mvc;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers.WriterPanelContollers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());

        public ActionResult MyContent()
        {
            var contentValues = contentManager.GetListByWriter();
            return View(contentValues);
        }
    }
}
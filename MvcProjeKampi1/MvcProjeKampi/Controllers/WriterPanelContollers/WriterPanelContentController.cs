using System;
using System.Linq;
using System.Text;
using BusinessLayer.Concrete;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKampi.Controllers.WriterPanelContollers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());

        Context context = new Context();

        public ActionResult MyContent(string session)
        {
            session = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId).FirstOrDefault();
            var contentValues = contentManager.GetListByWriter(writerIdInfo);
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            string session = (string)Session["WriterMail"];
            var writeridinfo = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId).FirstOrDefault();
            content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            content.WriterId = writeridinfo;
            content.ContentStatus = true;
            contentManager.ContentAdd(content);
            return RedirectToAction("MyContent");
        }

        public ActionResult ToDoList()
        {
            return View();
        }
    }
}
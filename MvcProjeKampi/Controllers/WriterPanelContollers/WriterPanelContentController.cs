using System;
using System.Linq;
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

        public ActionResult MyContent(string parameter)
        {
          
  
            parameter = (string) Session["WriterMail"]; //WriterMail bizim Session degerimiz
            var writerIdInfo = context.Writers.Where(x => x.WriterMail.ToString() == parameter).Select(y => y.WriterId)
                .FirstOrDefault();
            var contentValues = contentManager.GetListByWriter(writerIdInfo);
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.d = id;
            return View();
        }


        [HttpPost]
        public ActionResult AddContent(Content content)
        {
            string mail = (string)Session["WriterEmail"];
            var writeridinfo = context.Writers.Where(w => w.WriterMail.ToString() == mail).Select(x => x.WriterId).FirstOrDefault();
            content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            content.WriterId = writeridinfo;
            content.ContentStatus = true;
            contentManager.ContentAdd(content);
            return RedirectToAction("MyContent");
        }
    }
}
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {

        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());

        public ActionResult Index()
        {
            var headingValues = headingManager.GetList();
            return View(headingValues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> _valueCategory = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            List<SelectListItem> _valueWriter = (from x in writerManager.GetList()
                                                 select new SelectListItem()
                                                 {
                                                     Text = x.WriterName + " " + x.WriterSurName,
                                                     Value = x.WriterId.ToString()
                                                 }).ToList();
            ViewBag.valueCategory = _valueCategory;
            ViewBag.valueWriter = _valueWriter;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            headingManager.HeadingAdd(heading);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> _valueCategory = (from x in categoryManager.GetList()
                select new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            ViewBag.valueCategory = _valueCategory; //Her basligin bir kategorisi olacak
            var headingValue = headingManager.GetByIdHeading(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            headingManager.HeadingUpdate(heading);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headingManager.GetByIdHeading(id);
            headingValue.HeadingStatus = false;
            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("Index");
        }
    }
}
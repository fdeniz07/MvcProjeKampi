using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {

        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        StatusManager statusManager = new StatusManager(new EfStatusDal());

        public ActionResult Index(int? page) //Buradaki int? page bos gelmeye karsi önlem amaclidir
        {
            var headingValues = headingManager.GetList().ToPagedList(page ?? 1, 8); //? işaretleri boş gelme/boş olma durumuna karşı önlem                                                                                  amaçlı,kacinci sayfadan baslasin, sayfada kac deger olsun anlamina gelmektedir.
            return View(headingValues);
        }

        public ActionResult HeadingReport()
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

            List<SelectListItem> headingStatusValue = (from x in statusManager.GetList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.StatusName,
                                                           Value = x.StatusId.ToString()
                                                       }).ToList();

            ViewBag.valueHeadingStatus = headingStatusValue;
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

            List<SelectListItem> headingStatusValue = (from x in statusManager.GetList()
                                                       select new SelectListItem
                                                       {
                                                           Text = x.StatusName,
                                                           Value = x.StatusId.ToString()
                                                       }).ToList();


            ViewBag.valueHeadingStatus = headingStatusValue;
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

            if (headingValue.StatusId == 2)
            {
                headingValue.StatusId = 1;
            }
            else
            {
                headingValue.StatusId = 2;
            }
            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("Index");
        }

        public ActionResult HeadingByCategory(int id)
        {
            var headingValue = headingManager.GetListByCategoryId(id);
            return View(headingValue);
        }

        public ActionResult HeadingByWriter(int id)
        {
            var headingValue = headingManager.GetListByWriterId(id);
            return View(headingValue);
        }
    }
}
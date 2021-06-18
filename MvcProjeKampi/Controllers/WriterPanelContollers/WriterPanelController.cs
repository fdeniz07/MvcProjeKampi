using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading(int id)
        {
            var values = headingManager.GetListByWriterId(id);
            return View();
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> _valueCategory = (from x in categoryManager.GetList()
                select new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterId = 6; // --> Ileride buraya session gelecek
            headingManager.HeadingAdd(heading);
            return RedirectToAction("Index");
     
        }
    }
}
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Concrete;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        Context context = new Context();



        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading()
        {
            int id = 6; // --> Ileride buraya session gelecek
            List<Heading> values = headingManager.GetListByWriterId(id).ToList();
            return View(values);
        }

        //public ActionResult MyHeading(string session)
        //{
        //    session = (string)Session["WriterEmail"];
        //    var writerId = context.Writers.Where(x => x.WriterMail == session).Select(x => x.WriterId).FirstOrDefault();
        //    var values = headingManager.GetListByWriterId(writerId);
        //    return View(values);
        //}



        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> _valueCategory = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId.ToString()
                                                   }).ToList();
            ViewBag.valueCategory = _valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            HeadingValidator headingValidator = new HeadingValidator();
            ValidationResult results = headingValidator.Validate(heading);

            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterId = 6; // --> Ileride buraya session gelecek
            heading.HeadingStatus = true; // Yazar yeni baslik eklediginde baslangic degeri aktif olarak gelecek

            if (results.IsValid) 
            {
                headingManager.HeadingAdd(heading);
                return RedirectToAction("MyHeading");
            }
            else //Dogrulama islemi gecerli degilse;
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
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
            ViewBag.valueCategory = _valueCategory;
            var headingValue = headingManager.GetByIdHeading(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            headingManager.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headingManager.GetByIdHeading(id);
            headingValue.HeadingStatus = false;
            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }
    }
}
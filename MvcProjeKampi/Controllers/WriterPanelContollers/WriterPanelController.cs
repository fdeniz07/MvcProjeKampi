using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        StatusManager statusManager = new StatusManager(new EfStatusDal());

        HeadingValidator headingValidator = new HeadingValidator();
        WriterValidator writerValidator = new WriterValidator();

        Context context = new Context();

        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string session = (string)Session["WriterMail"];
            id = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId).FirstOrDefault();
            var writerValue = writerManager.GetById(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {
            ValidationResult results = writerValidator.Validate(writer);
            if (results.IsValid)
            {
                writer.WriterStatus = true;
                writerManager.WriterUpdate(writer);
                return RedirectToAction("AllHeadings", "WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        #region Eski Kodlar (Session Öncesi)

        //public ActionResult MyHeading()
        //{
        //    int id = 6; // --> Ileride buraya session gelecek
        //    List<Heading> values = headingManager.GetListByWriterId(id).ToList();
        //    return View(values);
        //}

        #endregion


        //[HttpGet]
        //public ActionResult WriterProfilePassword(int id = 0)
        //{
        //    string session = (string)Session["WriterMail"];
        //    id = context.Writers.Where(x => x.WriterMail == session).Select(z => z.WriterId).FirstOrDefault();
        //    var writerValue = writerManager.GetById(id);
        //    return View(writerValue);
        //}

        public PartialViewResult WriterPasswordChange(Writer writer, string currentPassword, string newPassword)
        {
            //writerManager.UpdatePassword(currentPassword, newPassword, writer);
            return PartialView();
        }


        //[HttpPost]
        //public ActionResult WriterProfilePassword(Writer writer, string currentPassword, string newPassword)
        //{
        //    writerManager.UpdatePassword(currentPassword,newPassword,writer);
        //    return RedirectToAction("WriterProfile");
        //}




        public ActionResult MyHeading(int? page) //Buradaki int? page bos gelmeye karsi önlem amaclidir
        {
            string session = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId).FirstOrDefault();
            //ViewBag.d = writerIdInfo;
            var values = headingManager.GetListByWriterId(writerIdInfo).ToPagedList(page ?? 1, 8);
            return View(values);
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
            ViewBag.valueCategory = _valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            ValidationResult results = headingValidator.Validate(heading);

            string writerMailInfo = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == writerMailInfo)
                .Select(y => y.WriterId).FirstOrDefault();

            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterId = writerIdInfo;
            heading.StatusId = 2; // Yazar yeni baslik eklediginde baslangic degeri aktif olarak gelecek
            //heading.HeadingStatus = true; 

            heading.WriterId = writerIdInfo;
            //headingManager.HeadingAdd(heading);
            //return RedirectToAction("MyHeading");

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

            List<SelectListItem> writerStatusValue = (from x in statusManager.GetList()
                select new SelectListItem
                {
                    Text = x.StatusName,
                    Value = x.StatusId.ToString()
                }).ToList();

            ViewBag.valueWriterStatus = writerStatusValue;



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

            if (headingValue.StatusId==2) // Durumu Aktif mi?
            {
                headingValue.StatusId = 1; // Durumu pasif yap
            }
            else
            {
                headingValue.StatusId = 2; // Durumu aktif yap
            }

            headingManager.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");

        }

        public ActionResult AllHeadings(int? page) //Buradaki int? page bos gelmeye karsi önlem amaclidir
        {
            //ViewBag.counter = categoryManager.GetList().Count;
            //ViewBag.a = categoryManager.GetList();

            var headings = headingManager.GetList().ToPagedList(page ?? 1, 8); //? işaretleri boş gelme/boş olma durumuna karşı önlem amaçlı,kacinci sayfadan                                                                                                       baslasin, sayfada kac deger olsun anlamina gelmektedir.
            return View(headings);
        }

        #region BinaryToString(string mail)
        //public static string BinaryToString(string mail)
        //{
        //    List<Byte> byteList = new List<Byte>();
        //    for (int i = 0; i < mail.Length; i += 8)
        //    {
        //        byteList.Add(Convert.ToByte(mail.Substring(i, 8), 2));
        //    }
        //    return Encoding.ASCII.GetString(byteList.ToArray());
        //}
        #endregion



    }
}
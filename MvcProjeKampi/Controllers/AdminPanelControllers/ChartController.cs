using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using MvcProjeKampi.Models;
using MvcProjeKampi.Models.Charts;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryListPieChart()
        {
            return View();
        }

        public ActionResult HeadingListColumnChart()
        {
            return View();
        }

        public ActionResult WriterListLineChart()
        {
            return View();
        }

        // Kategori icerisindeki Baslik Sayisi Listesi \\

        #region Mimarisiz Kategori icerisindeki Baslik Listesi Grafigi

        public ActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet); //Kategory islemleri icin kullanilan bir yazim komutu
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> categoryClasses = new List<CategoryClass>();
            categoryClasses.Add(new CategoryClass()
            {
                CategoryName = "Yazılım",
                CategoryCount = 8
            });
            categoryClasses.Add(new CategoryClass()
            {
                CategoryName = "Gezi",
                CategoryCount = 4
            });
            categoryClasses.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 7
            });
            categoryClasses.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 3
            });
            return categoryClasses;
        }

        #endregion
        
        public ActionResult CategoryListChart()
        {
            return Json(CategoryList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> CategoryList()
        {
            List<CategoryClass> categoryClasses = new List<CategoryClass>();
            using (var context = new Context())
            {
                categoryClasses = context.Categories.Select(x => new CategoryClass
                {
                    CategoryName = x.CategoryName,
                    CategoryCount = x.Headings.Count()
                }).ToList();
            }
            return categoryClasses;
        }
        
        // Yazarlarin Baslik Sayisi Listesi \\

        #region Mimarili Yazar Baslik Sayisi Listesi Grafigi

        public ActionResult WriterListChart()
        {
            return Json(WriterList(), JsonRequestBehavior.AllowGet);
        }

        public List<WriterClass> WriterList()
        {
            List<WriterClass> writerClasses = new List<WriterClass>();
            using (var context = new Context())
            {
                writerClasses = context.Writers.Select(x => new WriterClass
                {
                    WriterName = x.WriterName,
                    WriterCount = x.Headings.Count()
                }).ToList();
            }
            return writerClasses;
        }

        #endregion


        // Basliklarin Icerik Sayisi Listesi \\

        #region Mimarili Baslik  Icerik Sayisi Listesi Grafigi

        public ActionResult HeadingListChart()
        {
            return Json(HeadingList(), JsonRequestBehavior.AllowGet);
        }

        public List<HeadingClass> HeadingList()
        {
            List<HeadingClass> headingClasses = new List<HeadingClass>();
            using (var context = new Context())
            {
                headingClasses = context.Headings.Select(x => new HeadingClass
                {
                    HeadingName = x.HeadingName,
                    HeadingCount = x.Contents.Count()
                }).ToList();
            }

            return headingClasses;
        }

        #endregion

    }
}
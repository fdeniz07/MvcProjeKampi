using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryList()
        {
            var categoryValues = categoryManager.GetList();
            return View(categoryValues);
        }

        //Sayfa yüklendiginde
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();  //Sayfa yüklendiginde View geri döndürülür
        }


        [HttpPost] //Sayfadaki bir butona basildiginda tetiklenir
        public ActionResult AddCategory(Category category)
        {
            // categoryManager.CategoryAdd(category);
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(category);

            if (results.IsValid) // Dogrulama islemi gecerli ise;
            {
                categoryManager.CategoryAdd(category);
                return RedirectToAction("GetCategoryList"); //Ekleme islemini tamamladiktan sonra beni GetCategoryList sayfasina yönlendir
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
    }
}
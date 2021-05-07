using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Web.Mvc;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal()); //Ileride Enttiy Framework den
                                                                                    //vazgecilirse burasi degistirilebilir
        
        public ActionResult Index()
        {
            var categoryValues = categoryManager.GetList();
            return View(categoryValues);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate((category));
            if (results.IsValid)
            {
                categoryManager.CategoryAdd((category));
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                        ModelState.AddModelError(item.PropertyName,item.ErrorMessage);
                }
            }
            return View();
        }
    }
}
using System.Web.Mvc;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager categoryManager= new CategoryManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryList()
        {
            var categoryValues = categoryManager.GetAll();
            return View(categoryValues);
        }

        public ActionResult AddCategory(Category category)
        {
            categoryManager.CategoryAdd(category);
            return RedirectToAction("GetCategoryList"); 
            //Ekleme islemini tamamladiktan sonra beni GetCategoryList metoda yönlendir
        }
    }
}
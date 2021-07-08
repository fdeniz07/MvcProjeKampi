﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System.Web.Mvc;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;

namespace MvcProjeKampi.Controllers
{
   /* [Authorize(Roles = "B")]*/ // Sadece "B" rolüne sahip kisiler görüntüleyebilsin
    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal()); //Ileride Enttiy Framework'den vazgecilirse burasi degistirilebilir

        
        public ActionResult Index(int? page)
        {
            var categoryValues = categoryManager.GetList().ToPagedList(page ?? 1, 10); //? işaretleri boş gelme/boş olma durumuna                                                        karşı önlem amaçlı,kacinci sayfadan baslasin, sayfada kac deger olsun anlamina gelmektedir.
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
            ValidationResult results = categoryValidator.Validate(category);
            if (results.IsValid)
            {
                categoryManager.CategoryAdd(category);
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

        public ActionResult DeleteCategory(int id)
        {
            var categoryValue = categoryManager.GetByIdCategory(id);
            categoryManager.CategoryDelete(categoryValue);
            return RedirectToAction("Index");
        }

        [HttpGet] // Sayfa yüklendiginde calisacak
        public ActionResult UpdateCategory(int id)
        {
            var categoryValue = categoryManager.GetByIdCategory(id);
            return View(categoryValue); // geriye ilgili id degerini döndürecek
        }

        [HttpPost] // Güncelleme butonuna basildiginda tetiklenecek HttpPost islemi
        public ActionResult UpdateCategory(Category category)
        {
            categoryManager.CategoryUpdate(category);
            return RedirectToAction("Index");
        }

    }
}
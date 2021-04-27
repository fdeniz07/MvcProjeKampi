using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CategoryManager
    {
        GenericRepository<Category> repo = new GenericRepository<Category>();

        public List<Category> GetAll() // Hepsini Listeleme
        {
            return repo.List();
        }

        public void CategoryAdd(Category category) // Bu metod gecici olusturulmustur. Daha sonra Validation islemleri yapilacaktir.
        {
            //if (category.CategoryName == "" || category.CategoryName.Length <= 3 || category.CategoryDescription == "" || category.CategoryName.Length >= 51)
            //{
            //    hata mesaji
            //}
            //else
            //{
              repo.Insert(category);
            //}
        }
    }
}

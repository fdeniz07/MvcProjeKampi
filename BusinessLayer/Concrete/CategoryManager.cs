using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Update(category); // GenericRepository'den gelen Delete() metodu
        }

        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        public Category GetByIdCategory(int id)
        {
            return _categoryDal.Get(x => x.CategoryId == id); // DAL dan gelen ilgili alani karsilar
        }

        public List<Category> GetList()
        {
            return _categoryDal.List();
        }
    }
}

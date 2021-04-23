using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDal
    {
        //CRUD islemleri
        //Type Name();

        List<Category> List();

        void Insert(Category category);
        
        void Update(Category category);

        void Delete(Category category);

    }
}

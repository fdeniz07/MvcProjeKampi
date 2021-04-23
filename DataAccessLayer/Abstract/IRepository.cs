using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
   public interface IRepository<T>
   {
       List<T> List();

       void Insert(T entity);

       void Delete(T entity);

       void Update(T entity);
   }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();

        void Insert(T entity);

        T Get(Expression<Func<T, bool>> filter);

        void Delete(T entity);

        void Update(T entity);

        List<T> List(Expression<Func<T, bool>> filter);
    }
}

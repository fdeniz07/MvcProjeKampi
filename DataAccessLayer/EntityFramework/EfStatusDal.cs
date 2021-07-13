﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfStatusDal : GenericRepository<Status>, IStatusDal
    {
    }
}

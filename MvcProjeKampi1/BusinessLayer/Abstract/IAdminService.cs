using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();

        void AdminAdd(Admin admin);

        void AdminDelete(Admin admin);

        void AdminUpdate(Admin admin);

        Admin GetByAdminID(int id);
    }
}

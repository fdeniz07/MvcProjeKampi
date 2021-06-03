using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IContactService
    {
        List<Contact> GetList();
        void ContactAdd(Contact contact);
        Contact GetByIdContact(int id);
        void ContactDelete(Contact contact);
        void ContactUpdate(Contact contact);
    }
}

using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        private IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public void ContactAdd(Contact contact)
        {
            _contactDal.Insert(contact);
        }

        public void ContactDelete(Contact contact)
        {
            _contactDal.Delete(contact);
        }

        public void ContactUpdate(Contact contact)
        {
            _contactDal.Update(contact);
        }

        public Contact GetByIdContact(int id)
        {
            return _contactDal.Get(x => x.ContactId == id);
        }

        public Contact GetByIdContactAndSetRead(int id, bool read)
        {
            return _contactDal.Get(x => x.ContactId == id && x.IsRead==true);
        }

        public List<Contact> GetList()
        {
            return _contactDal.List();
        }
    }
}

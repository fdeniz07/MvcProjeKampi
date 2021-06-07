using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByIdMessage(int id)
        {
            return _messageDal.Get(x=>x.MessageId==id);
        }

        public List<Message> GetList()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com"); //ileride degistirilecek
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
           _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
           _messageDal.Update(message);
        }
    }
}

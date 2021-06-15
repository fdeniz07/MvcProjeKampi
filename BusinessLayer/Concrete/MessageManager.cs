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
            return _messageDal.Get(x => x.MessageId == id);
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x => x.IsDraft == true);
        }

        public List<Message> GetListImportant()
        {
            return _messageDal.List(x => x.IsImportant == true && x.ReceiverMail == "admin@gmail.com");
        }

        public List<Message> GetListInbox()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com"); //ileride degistirilecek
        }

        public List<Message> GetListSendbox()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com");
        }

        public List<Message> GetListSpam()
        {
            return _messageDal.List(x => x.IsSpam == true);
        }

        public List<Message> GetListTrash()
        {
            return _messageDal.List(x => x.Trash == true);
        }

        public List<Message> GetReadList()
        {
            return _messageDal.List(x => x.IsRead == true && x.ReceiverMail == "admin@gmail.com");
        }

        public List<Message> GetUnReadList()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com" && x.IsRead == false);
        }

        public List<Message> IsDraft()
        {
            return _messageDal.List(x => x.IsDraft == true);
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

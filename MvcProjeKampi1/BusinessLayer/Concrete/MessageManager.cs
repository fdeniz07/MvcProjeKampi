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

        public List<Message> GetListDraft(string session)
        {
            return _messageDal.List(x => x.IsDraft == true && x.SenderMail == session);
        }

        public List<Message> GetListImportant(string session)
        {
            return _messageDal.List(x => x.IsImportant == true && x.ReceiverMail == session);
        }

        public List<Message> GetListInbox(string session)
        {
            return _messageDal.List(x => x.ReceiverMail == session);
        }

        public List<Message> GetListSendbox(string session)
        {
            return _messageDal.List(x => x.SenderMail == session);
        }

        public List<Message> GetListSpam(string session)
        {
            return _messageDal.List(x => x.IsSpam == true && x.ReceiverMail==session);
        }

        public List<Message> GetListTrash()
        {
            return _messageDal.List(x => x.Trash == true );
        }

        public List<Message> GetReadList(string session)
        {
            return _messageDal.List(x => x.IsRead == true && x.ReceiverMail == session);
        }

        public List<Message> GetUnReadList(string session)
        {
            return _messageDal.List(x => x.ReceiverMail == session && x.IsRead == false);
        }

        public List<Message> IsDraft(string session)
        {
            return _messageDal.List(x => x.IsDraft == true && x.SenderMail == session);
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

using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox(string session);
        List<Message> GetListSendbox(string session);
        List<Message> GetReadList(string session);
        List<Message> GetUnReadList(string session);
        List<Message> IsDraft(string session);
        List<Message> GetListDraft(string session);
        List<Message> GetListTrash();
        List<Message> GetListSpam(string session);
        List<Message> GetListImportant(string session);
        Message GetByIdMessage(int id);
        void MessageAdd(Message message);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);
    }
}

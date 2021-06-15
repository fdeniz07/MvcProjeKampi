using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox();
        List<Message> GetListSendbox();
        List<Message> GetReadList();
        List<Message> GetUnReadList();
        List<Message> IsDraft();
        List<Message> GetListDraft();
        List<Message> GetListTrash();
        List<Message> GetListSpam();
        List<Message> GetListImportant();
        Message GetByIdMessage(int id);
        void MessageAdd(Message message);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);
    }
}

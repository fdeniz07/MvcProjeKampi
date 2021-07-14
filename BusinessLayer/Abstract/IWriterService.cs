using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IWriterService
    {
        //void UpdatePassword(string currentPassword, string newPassword, Writer writer);
        List<Writer> GetList();
        List<Writer> GetListByWriterId(int id);
        Writer GetById(int id);
        void WriterAdd(Writer writer);
        void WriterDelete(Writer writer);
        void WriterUpdate(Writer writer);
        
    }
}

using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList();
        List<Content> GetListByHeadingId(int id); //Basliga göre icerik listeleme
        void ContentAdd(Content content);
        Content GetByIdContent(int id); 
        void ContentDelete(Content content);
        void ContentUpdate(Content content);
    }
}

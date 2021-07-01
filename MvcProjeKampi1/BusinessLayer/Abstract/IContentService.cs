using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetListBySearch(string searchKeyWord);
        List<Content> GetList();
        List<Content> GetListByWriter(int id);
        List<Content> GetListByHeadingId(int id); //Basliga göre icerik listeleme
        Content GetByIdContent(int id);
        void ContentAdd(Content content);
        void ContentDelete(Content content);
        void ContentUpdate(Content content);
    }
}

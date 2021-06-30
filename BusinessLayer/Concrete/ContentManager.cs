using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using DataAccessLayer.Abstract;

namespace BusinessLayer.Concrete
{
    public class ContentManager : IContentService
    {
        private IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public void ContentAdd(Content content)
        {
            _contentDal.Insert(content);
        }

        public void ContentDelete(Content content)
        {
            _contentDal.Delete(content);
        }

        public void ContentUpdate(Content content)
        {
            _contentDal.Update(content);
        }

        public Content GetByIdContent(int id)
        {
            return _contentDal.Get(x => x.ContentId == id);
        }

        public List<Content> GetListBySearch(string searchKeyWord)
        {
            return _contentDal.List(x=>x.ContentValue.Contains(searchKeyWord));
        }

        public List<Content> GetListByHeadingId(int id)
        {
            return _contentDal.List(x => x.HeadingId == id); // Dışarıdan gönderilen id,ye göre listeleme yapacak
        }

        public List<Content> GetListByWriter(int id)
        {
            return _contentDal.List(x => x.WriterId == id); 
        }

        public List<Content> GetList()
        {
            return _contentDal.List();
        }
    }
}

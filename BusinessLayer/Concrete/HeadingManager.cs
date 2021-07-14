using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class HeadingManager : IHeadingService
    {
        private IHeadingDal _headingDal; // DI - Dependency Injection Design Pattern

        public HeadingManager(IHeadingDal headingDal) // Constructor
        {
            _headingDal = headingDal;
        }


        public Heading GetByIdHeading(int id)
        {
            return _headingDal.Get(x => x.HeadingId == id);
        }

        public List<Heading> GetList()
        {
            return _headingDal.List();
        }

        public List<Heading> GetListByCategoryId(int id)
        {
            return _headingDal.List(x => x.CategoryId == id);
        }


        public List<Heading> GetListByWriterId(int id)
        {
            return _headingDal.List(x => x.WriterId == id);
        }

        public void HeadingAdd(Heading heading)
        {
            _headingDal.Insert(heading);
        }

        public void HeadingDelete(Heading heading)
        {
            _headingDal.Update(heading);
        }

        public void HeadingUpdate(Heading heading)
        {
            _headingDal.Update(heading);
        }
    }
}

using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IAboutService
    {
        List<About> GetList();
        void AboutAdd(About about);
        About GetByIdAbout(int id);
        void AboutDelete(About about);
        void AboutUpdate(About about);
    }
}

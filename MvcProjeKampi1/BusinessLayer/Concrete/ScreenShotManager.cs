using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
   public class ScreenShotManager:IScreenShotService
   {
       private IScreenShotDal _screenShotDal;

       public ScreenShotManager(IScreenShotDal screenShotDal)
       {
           _screenShotDal = screenShotDal;
       }

        public List<ScreenShot> GetList()
        {
            return _screenShotDal.List();
        }
    }
}

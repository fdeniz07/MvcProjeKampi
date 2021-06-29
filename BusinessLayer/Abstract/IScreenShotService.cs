using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IScreenShotService
    {
        List<ScreenShot> GetList();
    }
}

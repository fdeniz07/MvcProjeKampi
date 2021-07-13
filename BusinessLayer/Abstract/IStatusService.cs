using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IStatusService
    {
        List<Status> GetList();
    }
}

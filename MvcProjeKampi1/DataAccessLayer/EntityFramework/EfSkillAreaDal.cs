using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfSkillAreaDal : GenericRepository<SkillArea>, ISkillAreaDal
    {
    }
}

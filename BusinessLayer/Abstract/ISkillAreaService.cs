using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
   public interface ISkillAreaService
    {
        SkillArea GetByIdSkillArea(int id);
        List<SkillArea> GetList();
        void SkillAreaAdd(SkillArea skillArea);
        void SkillAreaDelete(SkillArea skillArea);
        void SkillAreaUpdate(SkillArea skillArea);
    }
}

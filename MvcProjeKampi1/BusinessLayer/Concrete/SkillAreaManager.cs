using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
   public class SkillAreaManager:ISkillAreaService
   {
       private ISkillAreaDal _skillAreaDal;

       public SkillAreaManager(ISkillAreaDal skillAreaDal)
       {
           _skillAreaDal = skillAreaDal;
       }

        public SkillArea GetByIdSkillArea(int id)
        {
            return _skillAreaDal.Get(x => x.SkillAreaId == id);
        }

        public List<SkillArea> GetList()
        {
            return _skillAreaDal.List();
        }

        public void SkillAreaAdd(SkillArea skillArea)
        {
            _skillAreaDal.Insert(skillArea);
        }

        public void SkillAreaDelete(SkillArea skillArea)
        {
            _skillAreaDal.Delete(skillArea);
        }

        public void SkillAreaUpdate(SkillArea skillArea)
        {
           _skillAreaDal.Update(skillArea);
        }
    }
}

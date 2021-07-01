using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Concrete
{
    public class TalentManager:ITalentService
   {
       private ITalentDal _talentDal;

       public TalentManager(ITalentDal talentDal)
       {
           _talentDal = talentDal;
       }

        public Talent GetByIdTalent(int id)
        {
            return _talentDal.Get(x=>x.SkillId==id);
        }

        public List<Talent> GetList()
        {
            return _talentDal.List();
        }

        public void TalentAdd(Talent talent)
        {
          _talentDal.Insert(talent);
        }

        public void TalentDelete(Talent talent)
        {
           _talentDal.Delete(talent);
        }

        public void TalentUpdate(Talent talent)
        {
           _talentDal.Update(talent);
        }
    }
}

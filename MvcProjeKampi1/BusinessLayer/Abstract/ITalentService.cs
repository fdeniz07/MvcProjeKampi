using EntityLayer.Concrete;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ITalentService
    {
        List<Talent> GetList();
        Talent GetByIdTalent(int id); 
        void TalentAdd(Talent talent);
        void TalentDelete(Talent talent);
        void TalentUpdate(Talent talent);
    }
}

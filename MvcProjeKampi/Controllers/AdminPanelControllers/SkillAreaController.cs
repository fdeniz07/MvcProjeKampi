using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class SkillAreaController : Controller
    {
        SkillAreaManager skillAreaManager = new SkillAreaManager(new EfSkillAreaDal());

        public ActionResult Index()
        {
            var skillAreaValues = skillAreaManager.GetList();
            return View(skillAreaValues);
        }


        [HttpGet]
        public ActionResult AddSkillArea()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSkillArea(SkillArea skillArea)
        {
            skillAreaManager.SkillAreaAdd(skillArea);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTalent(int id)
        {
            var talentValues = skillAreaManager.GetByIdSkillArea(id);
            return View(talentValues);
        }

        public ActionResult UpdateTalent(SkillArea skillArea)
        {
            skillAreaManager.SkillAreaUpdate(skillArea);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTalent(int Id)
        {
            var talentValues = skillAreaManager.GetByIdSkillArea(Id);
            skillAreaManager.SkillAreaDelete(talentValues);
            return RedirectToAction("Index");
        }



    }
}
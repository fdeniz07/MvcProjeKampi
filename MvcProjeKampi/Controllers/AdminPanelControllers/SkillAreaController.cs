using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Web.Mvc;
using PagedList;

namespace MvcProjeKampi.Controllers
{
    public class SkillAreaController : Controller
    {
        SkillAreaManager skillAreaManager = new SkillAreaManager(new EfSkillAreaDal());

        public ActionResult Index(int? page)
        {
            var skillAreaValues = skillAreaManager.GetList().ToPagedList(page ?? 1, 8); //? işaretleri boş gelme/boş olma durumuna 
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

        [HttpGet]
        public ActionResult UpdateSkillArea(int id)
        {
            var talentValues = skillAreaManager.GetByIdSkillArea(id);
            return View(talentValues);
        }

        [HttpPost]
        public ActionResult UpdateSkillArea(SkillArea skillArea)
        {
            skillAreaManager.SkillAreaUpdate(skillArea);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSkillArea(int Id)
        {
            var talentValues = skillAreaManager.GetByIdSkillArea(Id);
            skillAreaManager.SkillAreaDelete(talentValues);
            return RedirectToAction("Index");
        }



    }
}
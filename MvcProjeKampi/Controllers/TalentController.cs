using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class TalentController : Controller
    {

        TalentManager talentManager = new TalentManager(new EfTalentDal());
        SkillAreaManager skillAreaManager = new SkillAreaManager(new EfSkillAreaDal());

        TalentValidator talentValidator = new TalentValidator();

        public ActionResult Index()
        {
            var talentValues = talentManager.GetList();
            return View(talentValues);
        }

        public ActionResult TalentCard()
        {
            var talentValues = talentManager.GetList();
            return View(talentValues);
        }

        [HttpGet]
        public ActionResult AddTalent()
        {
            List<SelectListItem> _valueSkill = (from x in skillAreaManager.GetList()
                select new SelectListItem
                {
                    Text = x.Area,
                    Value = x.SkillAreaId.ToString()
                }).ToList();
            ViewBag.valueSkill = _valueSkill;

            return View();
        }

        [HttpPost]
        public ActionResult AddTalent(Talent talent, string addTalent)
        {
            //ValidationResult results = talentValidator.Validate(talent);

            //if (addTalent == "newTalent")
            //{
            //    if (results.IsValid)
            //    {
            //        talentManager.TalentAdd(talent);
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        foreach (var item in results.Errors)
            //        {
            //            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //        }
            //    }
            //}

            talentManager.TalentAdd(talent);
            return RedirectToAction("Index");
        }

        public ActionResult UpdateTalent(int id)
        {
            var talentValues = talentManager.GetByIdTalent(id);
            return View(talentValues);
        }

        public ActionResult UpdateTalent(Talent talent)
        {
            talentManager.TalentUpdate(talent);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTalent(int Id)
        {
            var talentValues = talentManager.GetByIdTalent(Id);
            talentManager.TalentDelete(talentValues);
            return RedirectToAction("Index");
        }

    }
}
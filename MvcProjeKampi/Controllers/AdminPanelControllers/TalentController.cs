using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
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

        public ActionResult Index(int? page)
        {
            var talentValues = talentManager.GetList().ToPagedList(page ?? 1, 6); //? işaretleri boş gelme/boş olma durumuna 
            return View(talentValues);
        }

        public ActionResult TalentCard(int? page)
        {
            var talentValues = talentManager.GetList().ToPagedList(page ?? 1, 8);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult AddTalent(Talent talent)//, string addTalent
        {
            ValidationResult results = talentValidator.Validate(talent);

            //if (addTalent == "newTalent")
            //{
                if (results.IsValid)
                {
                    talentManager.TalentAdd(talent);
                    return RedirectToAction("AddTalent");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            //}
            return View();
        }

        [HttpGet]
        public ActionResult UpdateTalent(int id)
        {
            List<SelectListItem> _valueSkill = (from x in skillAreaManager.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.Area,
                                                    Value = x.SkillAreaId.ToString()
                                                }).ToList();
            ViewBag.valueSkill = _valueSkill;
            var talentValues = talentManager.GetByIdTalent(id);
            return View(talentValues);
        }
        [HttpPost]
        public ActionResult UpdateTalent(Talent talent)
        {
            talentManager.TalentUpdate(talent);
            return RedirectToAction("TalentCard");
        }

        public ActionResult DeleteTalent(int Id)
        {
            var talentValues = talentManager.GetByIdTalent(Id);
            talentManager.TalentDelete(talentValues);
            return RedirectToAction("TalentCard");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MvcProjeKampi.Models;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{
    [AllowAnonymous]
    public class ScheduleController : Controller
    {
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View(new Schedule());
        }

        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var viewModel = new Schedule();
            var events = new List<Schedule>();
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-14);

            foreach (var item in headingManager.GetList())
            {
                events.Add(new Schedule()
                {
                    title = item.HeadingName,
                    start = item.HeadingDate,
                    end = item.HeadingDate.AddDays(-14),
                    allDay = false
                });

                start = start.AddDays(7);
                end = end.AddDays(7);
            }


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Agenda()
        {
            return View();
        }
    }
}
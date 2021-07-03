using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using PagedList;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{
    public class AuthorizationController : Controller
    {

        AdminManager adminManager = new AdminManager(new EfAdminDal());

        public ActionResult Index(int? page)
        {
            var adminValues = adminManager.GetList().ToPagedList(page ?? 1, 8); ;
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            List<SelectListItem> adminValue = (from x in adminManager.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AdminRole,
                                                   Value = x.AdminId.ToString()
                                               }).ToList();
            ViewBag.valueAdmin = adminValue;
            return View(adminValue);
        }

        [HttpPost]
        public ActionResult UpdateRole(Admin admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAdmin(int id)
        {
            var adminValue = adminManager.GetByAdminID(id);

            if (adminValue.AdminStatus)
            {
                adminValue.AdminStatus = false;
            }
            else
            {
                adminValue.AdminStatus = true;
            }
            adminManager.AdminDelete(adminValue);
            return RedirectToAction("Index");
        }


        //public PartialViewResult AuthorizationPartial()
        //{
        //    return PartialView();
        //}

    }
}
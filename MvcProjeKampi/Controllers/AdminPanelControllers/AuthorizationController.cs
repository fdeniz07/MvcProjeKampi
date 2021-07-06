using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using PagedList;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{
    public class AuthorizationController : Controller
    {

        AdminManager adminManager = new AdminManager(new EfAdminDal());
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()));
        RoleManager roleManager = new RoleManager(new EfRoleDal());

        public ActionResult Index(int? page)
        {
            var adminValues = adminManager.GetList().ToPagedList(page ?? 1, 8);
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> adminRoleValue = (from x in roleManager.GetRoles() 
                                               select new SelectListItem
                                               {
                                                   Text = x.RoleName,
                                                   Value = x.RoleId.ToString()
                                               }).ToList();
            ViewBag.valueAdminRole = adminRoleValue;
            return View();
        }


        [HttpPost]
        public ActionResult AddAdmin(AdminLogInDto adminLogInDto)
        {
            authService.AdminRegister(adminLogInDto.AdminUserName, adminLogInDto.AdminMail, adminLogInDto.AdminPassword,adminLogInDto.AdminRoleId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            List<SelectListItem> adminRoleValue = (from x in roleManager.GetRoles()
                                               select new SelectListItem
                                               {
                                                   Text = x.RoleName,
                                                   Value = x.RoleId.ToString()
                                               }).ToList();
            ViewBag.valueAdminRole = adminRoleValue;
            var adminValue = adminManager.GetByAdminID(id);
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


        public PartialViewResult AuthorizationPartial()
        {
            return PartialView();
        }
    }
}
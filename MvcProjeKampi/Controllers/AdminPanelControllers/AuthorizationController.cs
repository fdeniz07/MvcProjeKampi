using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers.AdminPanelControllers
{
    public class AuthorizationController : Controller
    {

        AdminManager adminManager = new AdminManager(new EfAdminDal());
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()));
        RoleManager roleManager = new RoleManager(new EfRoleDal());
        StatusManager statusManager = new StatusManager(new EfStatusDal());


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
                                                       Text = x.Description,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();

            List<SelectListItem> adminStatusValue = (from x in statusManager.GetList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.StatusName,
                                                         Value = x.StatusId.ToString()
                                                     }).ToList();
            ViewBag.valueAdminStatus = adminStatusValue;
            ViewBag.valueAdminRole = adminRoleValue;
            return View();
        }


        [HttpPost]
        public ActionResult AddAdmin(AdminLogInDto adminLogInDto)
        {
            authService.AdminRegister(adminLogInDto.AdminUserName, adminLogInDto.AdminMail, adminLogInDto.AdminPassword,
                adminLogInDto.AdminRoleId, adminLogInDto.AdminStatusId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            List<SelectListItem> adminRoleValue = (from x in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Description,
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

            adminManager.AdminDelete(adminValue);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeAdminStatus(int id)
        {
            var adminValue = adminManager.GetByAdminID(id);

            if (adminValue.StatusId==2) // Durumu Aktif mi?
            {
                adminValue.StatusId = 1; // Durumu pasif yap
            }
            else
            {
                adminValue.StatusId=2; // Durumu aktif yap
            }

            adminManager.AdminUpdate(adminValue);
            return RedirectToAction("Index");

        }

        public PartialViewResult AuthorizationPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult UpdateProfile(int id, AdminLogInDto adminLogInDto)
        {
            List<SelectListItem> adminStatusValue = (from x in statusManager.GetList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.StatusName,
                                                         Value = x.StatusId.ToString()
                                                     }).ToList();



            List<SelectListItem> adminRoleValue = (from x in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Description,
                                                       Value = x.RoleId.ToString()
                                                   }).ToList();

            //var admin = _adminService.GetList();
            //var adminValue = authService.AdminLogIn(adminLogInDto);
            ViewBag.valueAdminStatus = adminStatusValue;
            ViewBag.valueAdminRole = adminRoleValue;
            var adminValue = adminManager.GetByAdminID(id);
            return View(adminValue);
        }

        [HttpPost]
        public ActionResult UpdateProfile(Admin admin)
        {
            admin.StatusId = 2;
            adminManager.AdminUpdate(admin);

            return RedirectToAction("Index");
        }
    }
}
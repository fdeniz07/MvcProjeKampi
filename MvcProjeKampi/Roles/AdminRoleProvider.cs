using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Text;
using System.Web.Security;

namespace MvcProjeKampi.Roles
{
    public class AdminRoleProvider : RoleProvider
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string adminMail)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var adminMailCrypto = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                var admin = adminManager.GetList();
                foreach (var item in admin)
                {
                    for (int i = 0; i < adminMailCrypto.Length; i++)
                    {
                        if (adminMailCrypto[i] == item.AdminMail[i])
                        {
                            return new string[] { item.AdminRole };
                        }
                    }
                }
                return new string[] { };
            }

            //Bu metot, kullanıcılar için rol alma işine yarar  // Bu alan kurumsal mimariye uygun hale getirilecek (Ödev)
            //Context context = new Context(); 
            //var role = context.Admins.FirstOrDefault(x => x.AdminUserName == username);
            //return new string[] {role.AdminRole};
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
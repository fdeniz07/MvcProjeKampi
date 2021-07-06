using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Linq;
using System.Text;
using System.Web.Security;
using DataAccessLayer.Concrete;

namespace MvcProjeKampi.Roles
{
    public class AdminRoleProvider : RoleProvider
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());

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

        public override string[] GetRolesForUser(string username)
        {
            #region Eski Kodlar
            //Context context = new Context();
            //var result = context.Admins.FirstOrDefault(x => x.AdminMail.ToString() == username);
            //var resultWriter = context.Writers.FirstOrDefault(x => x.WriterMail.ToString() == username);

            //if (result != null)
            //{
            //    return new string[] { result.AdminRole };
            //}
            //else if (resultWriter != null)
            //{
            //    return new string[] { resultWriter.WriterRole };
            //}
            //return new string[] { };
            #endregion

            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailCrypto = crypto.ComputeHash(Encoding.UTF8.GetBytes(username));
                var admin = adminManager.GetList();
                var writer = writerManager.GetList();

                if (admin != null)
                {
                    foreach (var item in admin)
                    {
                        for (int i = 0; i < mailCrypto.Length; i++)
                        {
                            if (mailCrypto[i] == item.AdminMail[i])
                            {
                                return new string[] { item.Role.RoleName };
                            }
                        }
                    }
                }
                //else if (writer != null)
                //{
                //    foreach (var item in writer)
                //    {
                //        for (int i = 0; i < mailCrypto.Length; i++)
                //        {
                //            if (mailCrypto[i] == item.WriterMail[i])
                //            {
                //                return new string[] { item.WriterRole };
                //            }
                //        }
                //    }
                //}
                return new string[] { };

                    //foreach (var item in admin)
                    //{
                    //    for (int i = 0; i < mailCrypto.Length; i++)
                    //    {
                    //        if (mailCrypto[i] == item.AdminMail[i])
                    //        {
                    //            return new string[] { item.AdminRole };
                    //        }
                    //    }
                    //}
                    //return new string[] { };
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
using System.Text;
using BusinessLayer.Abstract;
using CoreLayer.Utilities.Cryptos.Hashing;
using EntityLayer.Concrete;
using EntityLayer.Dto;


namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        IAdminService _adminService;
       // IWriterService _writerService;

        public AuthManager(IAdminService adminService) //,IWriterService writerService
        {
            _adminService = adminService;
            //_writerService = writerService;
        }

        public bool Login(LoginDto loginDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(loginDto.AdminMail));
                var admin = _adminService.GetList();
                foreach (var item in admin)
                {
                    if (HashingHelper.VerifyPasswordHash(loginDto.AdminMail, loginDto.AdminPassword, item.AdminMail,
                        item.AdminPasswordHash, item.AdminPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void Register(string adminMail, string password)
        {
            byte[] mailHash, passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(adminMail, password, out mailHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminMail = mailHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                AdminRole = "A"
            };
            _adminService.AdminAdd(admin);
        }
    }
}

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
        IWriterService _writerService;

        public AuthManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;
        }

        public AuthManager(IAdminService adminService)
        {
            _adminService = adminService;
           
        }

        public AuthManager(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public bool AdminLogIn(AdminLogInDto adminLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminLogInDto.AdminMail));
                var admin = _adminService.GetList();
                foreach (var item in admin)
                {
                    if (HashingHelper.AdminVerifyPasswordHash(adminLogInDto.AdminMail, adminLogInDto.AdminPassword, item.AdminMail,
                        item.AdminPasswordHash, item.AdminPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void AdminRegister(string adminUserName, string adminMail, string password,int adminRole,int status)
        {
            byte[] mailHash, passwordHash, passwordSalt;
            HashingHelper.AdminCreatePasswordHash(adminMail, password, out mailHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminUserName = adminUserName,
                AdminMail = mailHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                RoleId = adminRole,
                StatusId = status
            };
            _adminService.AdminAdd(admin);
        }

        //------------------------- WRITER -----------------------------\\

        public bool WriterLogIn(WriterLogInDto writerLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                //var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(writerLogInDto.WriterMail));
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash( writerLogInDto.WriterPassword, 
                        item.WriterPasswordHash, item.WriterPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        } 

        public void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerUserName, string writerMail, string password, bool WriterStatus)
        {
            byte[] mailHash, passwordHash, passwordSalt;
            HashingHelper.WriterCreatePasswordHash(password, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterName = writerName,
                WriterSurName = writerSurName,
                WriterTitle = writerTitle,
                WriterAbout = writerAbout,
                WriterImage = writerImage,
                WriterUserName = writerUserName,
                WriterMail = writerMail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterStatus = WriterStatus
            };
            _writerService.WriterAdd(writer);
        }

    }
}
//string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, 
//, bool WriterStatus
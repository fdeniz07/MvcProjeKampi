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

        public void AdminRegister(string adminUserName, string adminMail, string password)
        {
            byte[] mailHash, passwordHash, passwordSalt;
            HashingHelper.AdminCreatePasswordHash(adminMail, password, out mailHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminUserName = adminUserName,
                AdminMail = mailHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                AdminRole = "A"
            };
            _adminService.AdminAdd(admin);
        }

        //------------------------- WRITER -----------------------------\\

        public bool WriterLogIn(WriterLogInDto writerLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(writerLogInDto.WriterMail));
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLogInDto.WriterMail, writerLogInDto.WriterPassword, item.WriterMail,
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
            HashingHelper.WriterCreatePasswordHash(writerMail, password, out mailHash, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterName = writerName,
                WriterSurName = writerSurName,
                WriterTitle = writerTitle,
                WriterAbout = writerAbout,
                WriterImage = writerImage,
                WriterUserName = writerUserName,
                WriterMail = mailHash,
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
using EntityLayer.Concrete;
using EntityLayer.Dto;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        void AdminRegister(string adminUserName, string adminMail, string password,int adminRole,int status);
        bool AdminLogIn(AdminLogInDto adminLoginDto);

        void WriterRegister(string writerName, string writerSurName, string writerTitle, string writerAbout, string writerImage, string writerUserName, string writerMail, string password, bool WriterStatus);
        bool WriterLogIn(WriterLogInDto writerLogInDto);
        //bool WriterLogIn(Writer writer);
    }
}

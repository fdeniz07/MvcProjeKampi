using EntityLayer.Dto;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        void AdminRegister(string adminUserName, string adminMail, string password);
        bool AdminLogIn(AdminLogInDto adminLoginDto);

        void WriterRegister(string writerUserName, string writerMail, string password);
        bool WriterLogIn(WriterLogInDto writerLoginDto);

    }
}

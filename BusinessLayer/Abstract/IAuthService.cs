using EntityLayer.Dto;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        void Register(string adminMail, string password);
        bool Login(LoginDto loginDto);
    }
}

﻿using EntityLayer.Dto;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        void Register(string adminUserName, string adminMail, string password);
        bool Login(LoginDto loginDto);
    }
}
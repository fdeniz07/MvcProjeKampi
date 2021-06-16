using System;
using System.Text;

namespace CoreLayer.Utilities.Cryptos.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string adminMail, string password, out byte[] adminMailHash, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                adminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                
                Console.WriteLine(passwordSalt);
                Console.WriteLine(passwordHash);
            }
        }

        public static bool VerifyPasswordHash(string adminMail, string password, byte[] adminMailHash, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                var computedAdminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                for (int i = 0; i < computedAdminMailHash.Length; i++)
                {
                    if (computedAdminMailHash[i] != adminMailHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool VerifyPasswordHash(string adminMail, byte[] adminMailHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedAdminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
                for (int i = 0; i < computedAdminMailHash.Length; i++)
                {
                    if (computedAdminMailHash[i] != adminMailHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}


//public static void CreatePasswordHash(string mail, string password, out byte[] mailHash, out byte[] passwordHash, out byte[] passwordSalt)
//{
//    using (var crypto = new System.Security.Cryptography.HMACSHA512())
//    {
//        passwordSalt = crypto.Key;
//        passwordHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(password));
//        mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(mail));
//    }
//}

//public static bool VerifyPasswordHash(string mail, string password, byte[] mailHash, byte[] passwordHash, byte[] passwordSalt)
//{
//    using (var crypto = new System.Security.Cryptography.HMACSHA512(passwordSalt))
//    {
//        var computedPasswordHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(password));
//        for (int i = 0; i < computedPasswordHash.Length; i++)
//        {
//            if (computedPasswordHash[i] != passwordHash[i])
//            {
//                return false;
//            }
//        }

//        var computedMailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(mail));
//        for (int i = 0; i < computedMailHash.Length; i++)
//        {
//            if (computedMailHash[i] != mailHash[i])
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//}

//public static bool VerifyPasswordHash(string mail, byte[] mailHash)
//{
//    using (var hmac = new System.Security.Cryptography.HMACSHA512())
//    {
//        var computedUserNameHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(mail));
//        for (int i = 0; i < computedUserNameHash.Length; i++)
//        {
//            if (computedUserNameHash[i] != mailHash[i])
//            {
//                return false;
//            }
//        }
//        return true;
//    }
//}
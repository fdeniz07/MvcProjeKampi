using System;
using System.Security.Cryptography;
using System.Text;

namespace CoreLayer.Utilities.Cryptos.Hashing
{
    public class HashingHelper
    {

        //----------------------------------- ADMIN --------------------------------------------\\

        public static void AdminCreatePasswordHash(string adminMail, string password, out byte[] adminMailHash, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                adminMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminMail));
            }
        }

        public static bool AdminVerifyPasswordHash(string adminMail, string password, byte[] adminMailHash, byte[] passwordHash, byte[] passwordSalt)
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

        public static bool AdminVerifyPasswordHash(string adminMail, byte[] adminMailHash)
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

        public static string AdminPasswordDecode(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512Hasher = new SHA512Managed();
            byte[] hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(password));
            return Convert.ToBase64String(hashedDataBytes);
        }

        //----------------------------------- WRITER --------------------------------------------\\

        public static void WriterCreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
               // writerMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(writerMail));
            }
        }

        public static bool WriterVerifyPasswordHash( string password,  byte[] passwordHash, byte[] passwordSalt)
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

                //var computedWriterMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(writerMail));
                //for (int i = 0; i < computedWriterMailHash.Length; i++)
                //{
                //    if (computedWriterMailHash[i] != writerMailHash[i])
                //    {
                //        return false;
                //    }
                //}
                return true;
            }
        }

        //public static bool WriterVerifyPasswordHash(string writerMail, byte[] writerMailHash)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        var computedWriterMailHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(writerMail));
        //        for (int i = 0; i < computedWriterMailHash.Length; i++)
        //        {
        //            if (computedWriterMailHash[i] != writerMailHash[i])
        //            {
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //}
    }
}


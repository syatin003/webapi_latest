using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace Stepan.Repository
{
    public class PasswordUtilities
    {
        public static string CreatePassword(int length)
        {
            string str = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chArray = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                chArray[i] = str[random.Next(0, str.Length)];
            }
            return new string(chArray);
        }
        public static string CreatePasswordHash(string password, string salt)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, "sha1");
        }
        public static string CreateSalt()
        {
            return CreateSalt(0x30);
        }
        public static string CreateSalt(int size)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] data = new byte[size];
            provider.GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}
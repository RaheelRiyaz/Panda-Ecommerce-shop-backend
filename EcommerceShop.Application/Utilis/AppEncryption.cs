using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Utilis
{
    public class AppEncryption
    {
        public static string GeerateSalat()
        {
            var rn = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(rn);
        }

        public static string GenerateHashedPassword(string salt,string password)
        {
            var mixedPassword = string.Concat(salt, password);
            var bytes = Encoding.UTF8.GetBytes(mixedPassword);
            return Convert.ToBase64String(bytes);
        }

        public static bool ComparePassword(string dbPassword, string salt,string userPassword)
        {
            return dbPassword == GenerateHashedPassword(salt, userPassword);
        }
    }
}

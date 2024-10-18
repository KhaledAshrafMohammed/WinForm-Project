using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Eldokkan.pl
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            var inputHash = HashPassword(inputPassword);
            return hashedPassword == inputHash;
        }
    }
}

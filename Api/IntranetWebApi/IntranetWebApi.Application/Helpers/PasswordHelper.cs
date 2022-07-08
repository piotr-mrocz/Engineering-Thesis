using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Helpers
{
    public static class PasswordHelper
    {
        public static string SecurePassword(string password)
        {
            var saltPassword = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordEnhanced = BCrypt.Net.BCrypt.HashPassword(password, saltPassword, true);

            return passwordEnhanced;
        }

        public static bool ValidatePassword(string password, string userPassword)
        {
            var veryfiedPasswordEnhanced = BCrypt.Net.BCrypt.Verify(password, userPassword, true);

            return veryfiedPasswordEnhanced;
        }

        public static (string password, string hashPassword) GeneratePassword()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            var hashedPassword = PasswordHelper.SecurePassword(finalString);

            return (password: finalString, hashPassword: hashedPassword);
        }
    }
}

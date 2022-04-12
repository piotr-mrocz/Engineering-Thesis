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
            var hasPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var saltPassword = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordEnhanced = BCrypt.Net.BCrypt.HashPassword(hasPassword, saltPassword, true);

            return passwordEnhanced;
        }

        public static bool ValidatePassword(string password)
        {
            var passwordToVeryfyEnhancedHashed = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(), true);
            var veryfiedPasswordEnhanced = BCrypt.Net.BCrypt.Verify(password, passwordToVeryfyEnhancedHashed, true);

            return veryfiedPasswordEnhanced;
        }
    }
}

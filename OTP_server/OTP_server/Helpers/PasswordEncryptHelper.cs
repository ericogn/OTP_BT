using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace OTP_server.Helpers
{
    public static class PasswordEncryptHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}

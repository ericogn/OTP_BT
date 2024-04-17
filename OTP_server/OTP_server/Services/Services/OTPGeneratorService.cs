using Microsoft.Extensions.Caching.Memory;
using OTP_server.Context;
using OTP_server.Helpers;
using OTP_server.Models;
using OTP_server.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace OTP_server.Services.Services
{
    public class OTPGeneratorService : IOTPGeneratorService
    {
        private const int OTPLength = 8;

        private DatabaseContext databaseContext;
        public OTPGeneratorService(DatabaseContext DatabaseContext)
        {
            databaseContext = DatabaseContext;
        }

        public OTPEntity GenerateOTP(string email, int minutesActive)
        {
            if (!IsValidEmail(email)) throw new Exception("Email is not valid");

            var existingUser = databaseContext.Users.Where(x=> x.Email == email).FirstOrDefault();
            if (existingUser is not null) databaseContext.Remove(existingUser);

            var oneTimePassword = GenerateOTPString(OTPLength);
            byte[] salt;
            User user = new User
            {
                Email = email,
                ExpirationDate = DateTime.UtcNow.AddMinutes(minutesActive),
                HashedOTP = PasswordEncryptHelper.HashPassword(oneTimePassword),
            };

            databaseContext.Add(user);
            databaseContext.SaveChanges();

            return new OTPEntity { OTP = oneTimePassword };
        }

        private string GenerateOTPString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
                             .Select(s => s[new Random().Next(s.Length)])
                             .ToArray());
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}

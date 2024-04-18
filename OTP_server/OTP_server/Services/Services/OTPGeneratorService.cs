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
        private const int OTPLength = 6;

        private DatabaseContext databaseContext;
        public OTPGeneratorService(DatabaseContext DatabaseContext)
        {
            databaseContext = DatabaseContext;
        }

        public OTPEntity GenerateOTP(string email, int minutesActive)
        {
            if (!IsValidEmail(email)) throw new Exception("Email is not valid");

            //ensure that there are not more users with same email
            var existingUser = databaseContext.OneTimePasswordDetails.Where(x=> x.Email == email).FirstOrDefault();
            if (existingUser is not null) databaseContext.Remove(existingUser);

            var oneTimePasswordString = GenerateOTPString(OTPLength);

            OneTimePasswordDetails oneTimePasswordDetail = new OneTimePasswordDetails
            {
                Email = email,
                ExpirationDate = DateTime.Now.AddMinutes(minutesActive),
                HashedOTP = PasswordEncryptHelper.HashPassword(oneTimePasswordString),
            };

            databaseContext.Add(oneTimePasswordDetail);
            databaseContext.SaveChanges();

            return new OTPEntity { OTP = oneTimePasswordString };
        }

 
        private string GenerateOTPString(int length)
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
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

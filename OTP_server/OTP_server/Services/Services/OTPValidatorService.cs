using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OTP_server.Context;
using OTP_server.Helpers;
using OTP_server.Models;
using OTP_server.Services.Interfaces;
using static System.Net.WebRequestMethods;

namespace OTP_server.Services.Services
{
    public class OTPValidatorService : IOTPValidatorService
    {
        private DatabaseContext databaseContext;
        public OTPValidatorService(DatabaseContext DatabaseContext)
        {
            databaseContext = DatabaseContext;
        }

        bool IOTPValidatorService.ValidateOTP(string email,string otp)
        {
            if (string.IsNullOrEmpty(otp) || string.IsNullOrEmpty(email))
            {
                return false;
            }

            var user = databaseContext.OneTimePasswordDetails.Where(x => x.Email == email).FirstOrDefault();
            if(user is not null && user.HashedOTP is not null && user.ExpirationDate > DateTime.Now)
            {
                var passwordsMatch = PasswordEncryptHelper.VerifyPassword(otp, user.HashedOTP);
                if (passwordsMatch)
                {
                    // one time use, so we delete the record if the otp has been confirmed
                    databaseContext.OneTimePasswordDetails.Remove(user);
                    databaseContext.SaveChanges();
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}

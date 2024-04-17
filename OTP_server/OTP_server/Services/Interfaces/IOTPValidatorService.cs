using Microsoft.AspNetCore.Mvc;

namespace OTP_server.Services.Interfaces
{
    public interface IOTPValidatorService
    {
        public bool ValidateOTP(string email, string OTP);
    }
}

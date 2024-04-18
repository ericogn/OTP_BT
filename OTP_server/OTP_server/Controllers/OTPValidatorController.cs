using Microsoft.AspNetCore.Mvc;
using OTP_server.Services.Interfaces;

namespace OTP_server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OTPValidatorController : ControllerBase
    {
        private readonly IOTPValidatorService _otpValidator;

        public OTPValidatorController(IOTPValidatorService otpValidator)
        {
            _otpValidator = otpValidator;
        }


        [HttpPost]
        public bool ValidateOTP(string email, string otp)
        {
            return _otpValidator.ValidateOTP(email, otp);
        }
    }
}

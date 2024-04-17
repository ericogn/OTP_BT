using Microsoft.AspNetCore.Mvc;
using OTP_server.Services.Interfaces;

namespace OTP_server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OTPGeneratorController : ControllerBase
    {
        private readonly IOTPGeneratorService _otpGenerator;
        public OTPGeneratorController(IOTPGeneratorService otpGenerator)
        {
            _otpGenerator = otpGenerator;
        }

        [HttpGet]
        public IActionResult GenerateOTP(string email, int minutesActive)
        {
            try
            {
                var otpConfiguration = _otpGenerator.GenerateOTP(email,minutesActive);
                if (otpConfiguration == null) return BadRequest();
                return Ok(otpConfiguration);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

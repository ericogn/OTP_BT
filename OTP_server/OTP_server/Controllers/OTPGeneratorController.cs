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

        [HttpPost]
        public IActionResult GenerateOTP(string email, int minutesActive)
        {
            try
            {
                var otpEntity = _otpGenerator.GenerateOTP(email,minutesActive);
                if (otpEntity == null) return BadRequest();
                return Ok(otpEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

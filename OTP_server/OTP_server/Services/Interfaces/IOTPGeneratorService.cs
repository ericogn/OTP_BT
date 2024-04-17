using OTP_server.Models;

namespace OTP_server.Services.Interfaces
{
    public interface IOTPGeneratorService
    {
        public OTPEntity GenerateOTP(string email, int minutesActive);
    }
}

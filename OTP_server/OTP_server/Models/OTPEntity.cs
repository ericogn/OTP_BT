namespace OTP_server.Models
{
    public class OTPEntity
    {
        public string OTP { get; set; }
        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddMinutes(2);
    }
}

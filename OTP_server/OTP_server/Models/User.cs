using System.ComponentModel.DataAnnotations;

namespace OTP_server.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string HashedOTP { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}

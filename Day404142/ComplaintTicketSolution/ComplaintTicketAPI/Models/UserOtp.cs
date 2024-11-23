using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models
{
    public class UserOtp
    {

        [Key]
        public int Id { get; set; } 

        [Required]
        public string Email { get; set; } 

        [Required]
        public string Otp { get; set; } 

        public DateTime OtpExpiry { get; set; } 
    }
}

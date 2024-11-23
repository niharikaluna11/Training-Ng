using System.ComponentModel.DataAnnotations;
using ComplaintTicketAPI.Validations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class ForgotPasswordDto
    {

        [Required(ErrorMessage = "Username or Email is required")]
        public string UsernameOrEmail { get; set; }

        public string Otp { get; set; }

        public string NewPassword { get; set; }

       
        public string? ConfirmNewPassword { get; set; }
    }

   
}

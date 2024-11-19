using ComplaintTicketAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
  
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "UserName Or Email is required")]
        public string UsernameOrEmail { get; set; }
       

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

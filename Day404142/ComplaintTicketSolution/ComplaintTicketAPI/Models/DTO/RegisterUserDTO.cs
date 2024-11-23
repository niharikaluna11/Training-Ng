using ComplaintTicketAPI.Validations;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ComplaintTicketAPI.Models.DTO
{
    public class RegisterUserDto
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FName { get;  set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Username is required")]
       
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        
         public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        public string Otp { get; set; }
        public Type? Types { get; set; }

    }
}

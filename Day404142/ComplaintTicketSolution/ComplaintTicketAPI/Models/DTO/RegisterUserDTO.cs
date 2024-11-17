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
        [MinLength(2, ErrorMessage = "Username should be at least 2 characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password should be at least 5 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]

        [EmailValidator]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        public Type Types { get; set; }

    }
}

using ComplaintTicketAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class ProfileUpdateDTO
    {

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Profile Picture is required")]
        public IFormFile ProfilePicture { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]

      //  [EmailValidator]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Atleast One Preferences is required")]

        public string Preferences { get; set; }
       

    }
}


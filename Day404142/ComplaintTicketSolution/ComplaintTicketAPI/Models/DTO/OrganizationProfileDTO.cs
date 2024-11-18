using ComplaintTicketAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class OrganizationProfileDTO
    {

        [Required(ErrorMessage = "Company Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Profile Picture is required")]
        public IFormFile ProfilePicture { get; set; }

        [Required(ErrorMessage = "Type of Company is required")]
        public Type Types { get; set; }
        // Enum or string: Company 1, Government 2, Agent 3

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]

        [PhoneValidator]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]

        [EmailValidator]
        public string Email { get; set; }
    }
}


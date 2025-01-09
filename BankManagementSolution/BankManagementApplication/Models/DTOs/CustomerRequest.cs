using System.ComponentModel.DataAnnotations;

namespace BankManagementApplication.Models.DTOs
{
    public class CustomerRequestDTO
    {
        [Required(ErrorMessage = "Account number is required.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [MinLength(4, ErrorMessage = "First name must be at least 4 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [MinLength(4, ErrorMessage = "Last name must be at least 4 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        [MinLength(7, ErrorMessage = "Address must be at least 7 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Account type is required.")]
        public AccountType AccountType { get; set; }
    }
}

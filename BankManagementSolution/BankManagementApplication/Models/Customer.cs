using System.ComponentModel.DataAnnotations;

namespace BankManagementApplication.Models
{
    public enum CustomerStatus
    {
        Activated = 0, Deactivated = 1
    }
    public enum AccountType
    {
        Savings = 0,
        Checking = 1,
        FixedDeposit = 2
    }

    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public AccountType AccountType { get; set; }
        public CustomerStatus CStatus { get; set; } = CustomerStatus.Activated;

    }
}

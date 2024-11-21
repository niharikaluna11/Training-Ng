using ComplaintTicketAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class UsernameValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // If the value is null or empty, return validation error
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult("Username cannot be empty.");
            }

            var username = value.ToString();

            // Check minimum length
            if (username.Length < 5)
            {
                return new ValidationResult("Username must be at least 5 characters long.");
            }

            // Check if username contains exactly one '@', at least two letters, and at least two digits
            if (!Regex.IsMatch(username, @"^(?=^[^@]*@[^@]*$)(?=(.*[A-Za-z].*){2})(?=(.*\d.*){1}).+$"))
            {
                return new ValidationResult("Username must contain exactly one '@', at least two letters, and at least one digits.");
            }

            // Resolve IUserRepository from the validation context
            var userRepository = (IUserRepository?)validationContext.GetService(typeof(IUserRepository));
            if (userRepository == null)
            {
                throw new InvalidOperationException("IUserRepository could not be resolved. Ensure it is registered in the DI container.");
            }

            // Check uniqueness in the repository
            var isUsernameTaken = userRepository.UserExists(username).Result; // Avoid .Result in production; use async alternatives.
            if (isUsernameTaken)
            {
                return new ValidationResult("The username is already taken.");
            }

            // If all checks pass
            return ValidationResult.Success;
        }
    }
}

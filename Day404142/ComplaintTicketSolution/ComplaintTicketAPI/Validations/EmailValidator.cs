using ComplaintTicketAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class EmailValidator : ValidationAttribute
    {
        // Regular expression pattern for validating email format
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public EmailValidator() : base("Invalid email format.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // If the value is null or empty, skip the validation
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            // Validate the email format using the regular expression
            var email = value.ToString();
            if (!Regex.IsMatch(email, EmailPattern))
            {
                return new ValidationResult("The email address is not in a valid format.");
            }
            var userRepository = (IUserRepository?)validationContext.GetService(typeof(IUserRepository));
            if (userRepository == null)
            {
                throw new InvalidOperationException("IUserRepository could not be resolved. Ensure it is registered in the DI container.");
            }
            var isUsernameTaken = userRepository.EmailExists(email).Result; // Avoid .Result in production; use async alternatives.
            if (isUsernameTaken)
            {
                return new ValidationResult("Email already Exist .Please Login.");
            }


            // If email is valid
            return ValidationResult.Success;
        }
        }
}

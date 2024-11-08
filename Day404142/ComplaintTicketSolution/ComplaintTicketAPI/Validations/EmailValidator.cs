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

            // If email is valid
            return ValidationResult.Success;
        }
        }
}

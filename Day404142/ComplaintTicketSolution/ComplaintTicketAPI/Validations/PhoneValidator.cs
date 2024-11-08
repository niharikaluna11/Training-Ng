using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class PhoneValidator : ValidationAttribute
    {
        // Regular expression for validating a 10-digit phone number (can include dashes, spaces, or parentheses)
        private const string PhonePattern = @"^(\+?\d{1,2}\s?)?(\(?\d{3}\)?[\s\-]?)?[\d\-]{7,10}$";

        public PhoneValidator() : base("Invalid Phone Number format. Must be 10 digits.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // If the value is null or empty, skip the validation
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            // Validate the phone number format using the regular expression
            var phoneNumber = value.ToString();
            if (!Regex.IsMatch(phoneNumber, PhonePattern))
            {
                return new ValidationResult("The phone number is not in a valid format.");
            }

            // If phone number is valid
            return ValidationResult.Success;
        }
    }
}

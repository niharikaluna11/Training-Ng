using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class PhoneValidator : ICustomValidator
    {
        
        private const string PhonePattern = @"^(\+?\d{1,2}\s?)?(\(?\d{3}\)?[\s\-]?)?[\d\-]{7,10}$";
       
        public async Task<BaseResponseDTO> ValidateAsync(object value)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Number cannot be empty.",
                    ErrorCode = 400 // Bad Request
                };
            }

            // Validate the phone number format using the regular expression
            var phoneNumber = value.ToString();
            if (!Regex.IsMatch(phoneNumber, PhonePattern))
            {
               // return new ValidationResult("The phone number is not in a valid format.");
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "The phone number is not in a valid format..",
                    ErrorCode = 400 // Bad Request
                };
            }

            return new BaseResponseDTO
            {
                Success = true,
                Message = "Phone Number validation passed."
            };

        }
   
    }


}
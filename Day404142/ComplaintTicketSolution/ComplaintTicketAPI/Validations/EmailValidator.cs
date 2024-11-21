using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Validations
{
    public class EmailValidator : ICustomValidator
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private readonly IUserRepository _userRepository;

        public EmailValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponseDTO> ValidateAsync(object value)
        {
            var email = value?.ToString();

            // Check if email is empty or null
            if (string.IsNullOrEmpty(email))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Email cannot be empty.",
                    ErrorCode = 400 // Bad Request
                };
            }

            // Validate email format using Regex
            if (!Regex.IsMatch(email, EmailPattern))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "The email address is not in a valid format.",
                    ErrorCode = 400 // Bad Request
                };
            }

            // Check if the email already exists in the repository
            var emailExists = await _userRepository.EmailExists(email);
            if (emailExists)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "The email already exists. Please login.",
                    ErrorCode = 409 // Conflict
                };
            }

            // Return success if all validations pass
            return new BaseResponseDTO
            {
                Success = true,
                Message = "Email validation passed."
            };
        }
    }
}

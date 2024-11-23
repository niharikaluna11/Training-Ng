using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class PasswordValidator : Attribute
    {
        private readonly IUserRepository _userRepository;
        private const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        // Minimum 8 characters, at least one letter, one number, one special character

        public PasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponseDTO> ValidateAsync(object value, object username)
        {
            var password = value?.ToString();

            // Check if username is null or empty
            if (string.IsNullOrEmpty(password))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password cannot be empty.",
                    ErrorCode = 400
                };
            }

            // Check minimum length
            if (password.Length < 5)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password must be at least 5 characters long.",
                    ErrorCode = 400
                };
            }

            if (password.Length > 15)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password must be at most 15 characters long.",
                    ErrorCode = 400
                };
            }

            if (Regex.IsMatch(password, @"(.)\1\1"))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password cannot have three or more consecutive repeating characters.",
                    ErrorCode = 400
                };
                // return "Password cannot have three or more consecutive repeating characters."; 
            }
            if (Regex.IsMatch(password, @"123|abc|qwerty", RegexOptions.IgnoreCase))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password cannot contain sequential characters.",
                    ErrorCode = 400
                };
                //return "Password cannot contain sequential characters.";
            }

            // Check if username contains exactly one '@', at least two letters, and at least one digit
            if (!Regex.IsMatch(password, PasswordPattern))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password must be at least 8 characters long, contain at least one letter, one number, and one special character.",
                    ErrorCode = 400
                };
            }


            // Check if password is the same as the username
            if (password.Equals(username))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Password cannot be the same as the username.",
                    ErrorCode = 400 // Bad Request
                };
            }


            // If all validations pass, return success
            return new BaseResponseDTO
            {
                Success = true,
                Message = "Password validation passed."
            };
        }
    }
}

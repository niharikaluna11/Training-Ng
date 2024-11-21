using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using System.Text.RegularExpressions;

namespace ComplaintTicketAPI.Validations
{
    public class UsernameValidator : ICustomValidator
    {
        private readonly IUserRepository _userRepository;

        public UsernameValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<BaseResponseDTO> ValidateAsync(object value)
        {
            var username = value?.ToString();

            // Check if username is null or empty
            if (string.IsNullOrEmpty(username))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Username cannot be empty.",
                    ErrorCode = 400
                };
            }

            // Check minimum length
            if (username.Length < 5)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Username must be at least 5 characters long.",
                    ErrorCode = 400
                };
            }

            // Check if username contains exactly one '@', at least two letters, and at least one digit
            if (!Regex.IsMatch(username, @"^(?=^[^@]*@[^@]*$)(?=(.*[A-Za-z].*){2})(?=(.*\d.*){1}).+$"))
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Username must contain exactly one '@', at least two letters, and at least one digit.",
                    ErrorCode = 400
                };
            }

            // Check if the username already exists in the repository
            bool isUsernameTaken = await _userRepository.UserExists(username);
            if (isUsernameTaken)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "The username is already taken.",
                    ErrorCode = 409 // Conflict
                };
            }

            // If all validations pass, return success
            return new BaseResponseDTO
            {
                Success = true,
                Message = "Username validation passed."
            };
        }
    }
}

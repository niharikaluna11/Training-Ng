using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
using ComplaintTicketAPI.Validations;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace ComplaintTicketAPI.Services
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ForgotPasswordService> _logger;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordService(
            IUserRepository userRepository,
            ILogger<ForgotPasswordService> logger,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _emailSender = emailSender;
        }

        private void SendMail(string title, string email, string body)
        {
            var message = new Message(new string[] { email }, title, body);
            _emailSender.SendEmail(message);
        }

        // Method to send OTP to the user's email
        public async Task<BaseResponseDTO> SendPasswordResetLink(string UsernameorEmail)
        {
            var user = await _userRepository.GetByUsernameOrEmail(UsernameorEmail);

            if (user == null)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "No user found with this email or username.",
                    ErrorCode = 500 // Internal Server Error
                };
               // throw new ArgumentException("No user found with this email or username.");
            }

            // Generate a 6-digit OTP
            string otp = GenerateOtp();

            // Save the OTP and expiry time to the database
            user.ResetToken = otp;
            user.TokenExpiry = DateTime.Now.AddMinutes(10); // OTP valid for 10 minutes
            await _userRepository.UpdateUser(user);

            SendMail("Password Reset OTP", user.Email, $"Your OTP for password reset is: {otp} . Valid for 10 mins only");

            return new BaseResponseDTO
            {
                Success = true,
                Message = "The Otp is send to your registered mail"
            };

        }

        // Method to verify if the OTP is valid
        public async Task<BaseResponseDTO> VerifyResetOtp(string otp)
        {
            var user = await _userRepository.GetUserByResetToken(otp);

            if (user == null)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "User not found or OTP is invalid.",
                    ErrorCode = 404 // Not Found
                };
            }

            if (user.TokenExpiry < DateTime.Now)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "OTP has expired.",
                    ErrorCode = 400 // Bad Request
                };
            }

            return new BaseResponseDTO
            {
                Success = true,
                Message = "OTP is valid."
            };

        }

        // Method to reset the user's password
        public async Task<BaseResponseDTO> ResetPassword(string UsernameOrEmail, string otp, string newPassword, string confirmNewPassword)
        {

            var passwordValidator = new PasswordValidator(_userRepository);
            var passwordValidationResult = await passwordValidator.ValidateAsync(newPassword, UsernameOrEmail);

            if (!passwordValidationResult.Success)
            {
                return passwordValidationResult; // Return validation error directly
            }

            // Check if the new password and confirm password match
            if (newPassword != confirmNewPassword)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "New password and confirm password do not match.",
                    ErrorCode = 500 // Internal Server Error
                };
                //throw new ArgumentException("New password and confirm password do not match.");
            }

            // Get user by email or username (you can add a method to search by email or username)
            var user = await _userRepository.GetByUsernameOrEmail(UsernameOrEmail);

            if (user == null)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "No user found with this email or username.",
                    ErrorCode = 500 // Internal Server Error
                };
               // throw new ArgumentException("No user found with this email or username.");
            }

            // Validate OTP
            if (user.ResetToken != otp || user.TokenExpiry < DateTime.Now)
            {
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "Invalid or expired OTP.",
                    ErrorCode = 500 // Internal Server Error
                };
               // throw new ArgumentException("Invalid or expired OTP.");
            }

            // Hash the new password
            using var hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));

            // Update the user's password (hashed for security)
            user.Password = passwordHash;
            user.HashKey = hmac.Key;
            user.ResetToken = null; // Clear the OTP after successful reset
            user.TokenExpiry = null;

           
            // Update the user record in the database
            await _userRepository.UpdateUser(user);

            return new BaseResponseDTO
            {
                Success = true,
                Message = "The Password is succesfully reset"
            };

        }

        // Method to generate a random 6-digit OTP
        private string GenerateOtp()
        {
            var random = new Random();
            string otp = random.Next(100000, 999999).ToString(); // Generates a 6-digit OTP
            return otp;
        }
    }
}

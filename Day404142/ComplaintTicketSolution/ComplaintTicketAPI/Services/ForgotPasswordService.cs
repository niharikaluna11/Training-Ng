using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
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
        public async Task SendPasswordResetLink(string UsernameorEmail)
        {
            var user = await _userRepository.GetByUsernameOrEmail(UsernameorEmail);

            if (user == null)
            {
                throw new ArgumentException("No user found with this email or username.");
            }

            // Generate a 6-digit OTP
            string otp = GenerateOtp();

            // Save the OTP and expiry time to the database
            user.ResetToken = otp;
            user.TokenExpiry = DateTime.Now.AddMinutes(10); // OTP valid for 10 minutes
            await _userRepository.UpdateUser(user);

            // Send the OTP to the user's email
            SendMail("Password Reset OTP", user.Email, $"Your OTP for password reset is: {otp}");
        }

        // Method to verify if the OTP is valid
        public async Task<bool> VerifyResetOtp(string otp)
        {
            var user = await _userRepository.GetUserByResetToken(otp);

            return user != null && user.TokenExpiry >= DateTime.Now;
        }

        // Method to reset the user's password
        public async Task ResetPassword(string UsernameOrEmail, string otp, string newPassword, string confirmNewPassword)
        {
            // Check if the new password and confirm password match
            if (newPassword != confirmNewPassword)
            {
                throw new ArgumentException("New password and confirm password do not match.");
            }

            // Get user by email or username (you can add a method to search by email or username)
            var user = await _userRepository.GetByUsernameOrEmail(UsernameOrEmail);

            if (user == null)
            {
                throw new ArgumentException("No user found with this email or username.");
            }

            // Validate OTP
            if (user.ResetToken != otp || user.TokenExpiry < DateTime.Now)
            {
                throw new ArgumentException("Invalid or expired OTP.");
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

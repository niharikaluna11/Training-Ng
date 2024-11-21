using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using ComplaintTicketAPI.Validations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ComplaintTicketAPI.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ComplaintTicketContext _context;
        private readonly ILogger<UserProfileService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly string _uploadFolder= Path.Combine(Directory.GetCurrentDirectory(), "ProfilePicture/User"); 

        public UserProfileService(ComplaintTicketContext context,
             IEmailSender emailSender,
              ILogger<UserProfileService> logger)
        {

            _context = context;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<string> SaveFileAsync(IFormFile file)

        {

            if (file == null || file.Length == 0)

            {

                return null;

            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))

            {

                await file.CopyToAsync(stream);

            }

            return uniqueFileName;

        }


       

        public async Task<UserProfile> GetProfile(int userId)
        {
            // Retrieve the user profile and organization profile from the database
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            var orgprofile = await _context.Organizations.FirstOrDefaultAsync(p => p.UserId == userId);

            // If both profiles exist, return a combined profile
            if (profile != null && orgprofile != null)
            {
                return new UserProfile
                {
                    UserId = profile.UserId,
                    FirstName = profile.FirstName ?? orgprofile.Name, // If FirstName is null, use orgprofile.Name
                    
                    Address = profile.Address ?? orgprofile.Address // Add other fields from orgprofile as needed
                };
            }

            // If only profile is found, return it
            return profile;
        }


        private void SendMail(string title, string email, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        email },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }
        public async Task<UserProfile> UpdateProfile(int userId, ProfileUpdateDTO updateDto)
        {
            
            try
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

                var phoneValidator = new PhoneValidator();
                var phoneValidationResult = await phoneValidator.ValidateAsync(updateDto.Phone);

                if (!phoneValidationResult.Success)
                {
                    throw new Exception("invalid phone number");
                    // return phoneValidationResult; // Return validation error directly
                }
                if (profile != null)
                {


                    // Update profile properties
                    profile.FirstName = updateDto.FirstName;
                    profile.LastName = updateDto.LastName;
                    profile.Address = updateDto.Address;
                    profile.DateOfBirth = updateDto.DateOfBirth;
                    profile.Email = updateDto.Email;
                    profile.Phone = updateDto.Phone;
                    profile.Preferences = updateDto.Preferences;

                    profile.ProfilePicture = await SaveFileAsync(updateDto.ProfilePicture);


                    try
                    {
                        string body = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f0f8ff;
                                            color: #333;
                                        }}
                                        .header {{
                                            background-color: #0073e6;
                                            color: white;
                                            padding: 10px;
                                            text-align: center;
                                            font-size: 24px;
                                        }}
                                        .greeting {{
                                            font-size: 18px;
                                            color: #333;
                                        }}
                                        .content {{
                                            margin-top: 20px;
                                            color: #333;
                                        }}
                                        .footer {{
                                            margin-top: 20px;
                                            font-size: 14px;
                                            color: #555;
                                        }}
                                        .signature {{
                                            color: #0073e6;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='header'>
                                        <h1>Welcome to TicketSolve!</h1>
                                    </div>

                                    <p class='greeting'>Dear <strong>{profile.FirstName}</strong>,</p>
    
                                    <p class='content'>We are pleased to inform you that your account profile has been successfully updated.</p>
    
                                    <p class='content footer'>If you have any questions or need further assistance, please do not hesitate to contact us.</p>
    
                                    <p class='footer'>Best regards,<br/><span class='signature'>TicketSolve Team</span></p>
                                </body>
                                </html>";


                        string email = profile.Email;
                        SendMail("Your Account Has Been Created", email, body);
                    }
                    catch { throw new Exception("Mail Cannot be send Becuase of Invalid Mail"); }

                    await _context.SaveChangesAsync();
                }
                return profile; // Return the updated profile
            }
               catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user profile for user: {UserId}", userId);
                throw new Exception("Failed to create user profile");
            }
        }
    }
}

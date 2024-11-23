using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
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
        private readonly IMapper _mapper;
        public UserProfileService(ComplaintTicketContext context,
             IEmailSender emailSender,
              ILogger<UserProfileService> logger,
              IMapper mapper)
        {

            _context = context;
            _logger = logger;
            _emailSender = emailSender;
            _mapper = mapper;
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

        public async Task<BaseResponseDTO> UpdateProfile(int userId, ProfileUpdateDTO updateDto)
        {
            try
            {
                // Fetch the user profile based on userId
                var profiles = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

                if (profiles == null)
                {
                    // If no profile is found, return an error response
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage = "Profile not found for the provided user ID.",
                        ErrorCode = 404 // Not Found
                    };
                }

                // Validate the phone number
                var phoneValidator = new PhoneValidator();
                var phoneValidationResult = await phoneValidator.ValidateAsync(updateDto.Phone);

                if (!phoneValidationResult.Success)
                {
                    return phoneValidationResult; // Return validation error directly if phone validation fails
                }

                // Update profile properties with the values from the DTO
                profiles.FirstName = updateDto.FirstName;
                profiles.LastName = updateDto.LastName;
                profiles.Address = updateDto.Address;
                profiles.DateOfBirth = updateDto.DateOfBirth;
                profiles.Email = updateDto.Email;
                profiles.Phone = updateDto.Phone;
                profiles.Preferences = updateDto.Preferences;

               // _mapper.Map(updateDto, profiles);

                // If profile picture is provided, save it
                if (updateDto.ProfilePicture != null)
                {
                    profiles.ProfilePicture = await SaveFileAsync(updateDto.ProfilePicture);
                }

                // Send an email notifying the user of the profile update
                try
                {
                    string emailBody = GenerateProfileUpdateEmailBody(profiles.FirstName, profiles.Email);
                    SendMail("Your Account Has Been Updated", profiles.Email, emailBody);
                }
                catch (Exception emailEx)
                {
                    // Log email sending error and return a response to the client
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage = $"Email could not be sent: {emailEx.Message}",
                        ErrorCode = 500 // Internal Server Error
                    };
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return a success response with the updated profile data
                return new SuccessResponseDTO<UserProfile>
                {
                    Success = true,
                    Message = "Profile updated successfully.",
                    Data = profiles
                };
            }
            catch (Exception ex)
            {
                // Log the error for troubleshooting
                _logger.LogError(ex, "Error updating profile for user: {UserId}", userId);

                // Return a generic error response if an exception is thrown
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = "An unexpected error occurred while updating the profile.",
                    ErrorCode = 500 // Internal Server Error
                };
            }
        }


        private string GenerateProfileUpdateEmailBody(string firstName, string email)
        {
            return $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f0f8ff;
                color: #333;
                margin: 0;
                padding: 20px;
                line-height: 1.6;
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
                margin-top: 20px;
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
                font-weight: bold;
            }}
        </style>
    </head>
    <body>
        <div class='header'>
            <h1>Welcome to TicketSolve!</h1>
        </div>

        <p class='greeting'>Dear <strong>{firstName}</strong>,</p>

        <p class='content'>
            We are pleased to inform you that your account profile has been successfully updated. Your account is now ready for use.
        </p>

        <p class='content'>
            Should you have any questions or require assistance, please do not hesitate to contact us.
        </p>

        <p class='footer'>
            Best regards,<br/>
            <span class='signature'>TicketSolve Team</span>
        </p>
    </body>
    </html>";
        }


        public async Task<List<OrganizationDTO>> GetALLOrgProfile()
        {
            try
            {
                // Retrieve all organization profiles from the Organizations table
                var orgProfiles = await _context.Organizations.ToListAsync();
                return _mapper.Map<List<OrganizationDTO>>(orgProfiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving organization profiles.");
                throw new Exception("Failed to get organization profiles.", ex);
            }
        }

        public async Task<List<UserProfile>> GetALLUserProfile()
        {
            try
            {
                // Retrieve all organization profiles from the Organizations table
                var profiles = await _context.Profiles.ToListAsync();
                return _mapper.Map<List<UserProfile>>(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving organization profiles.");
                throw new Exception("Failed to get organization profiles.", ex);
            }
        }
    }
}

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

        public async Task<string> SaveFileAsync(IFormFile file, string username)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            // Create a file name that includes the username
            var uniqueFileName = $"{username}_{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Specify the folder path on GitHub where the file should be uploaded
            string folderPath = "profilepic/user";

            // Create an instance of GitHubService and upload the file
            var gitHubService = new GitHubService();
            var result = await gitHubService.SaveFileToGitHub(filePath, uniqueFileName, folderPath);

            return result;  // Return the result (success message or URL)
        }


        public async Task<ProfilePicDTO> GetProfilePic(string username)
        {
            // Fetch the user based on the username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            // Check if the user exists
            if (user == null)
            {
                return new ProfilePicDTO
                {
                    ProfilePicture = "" // Return default or empty picture
                };
            }

            // Fetch profile and organization profile for the user
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == user.userId);
            var orgprofile = await _context.Organizations.FirstOrDefaultAsync(p => p.UserId == user.userId);

            // Check if there's a local profile picture
            string profilePicUrl = profile?.ProfilePicture ?? orgprofile?.ProfilePicture;

            // If the profile picture exists in GitHub, use the GitHub URL
            if (!string.IsNullOrEmpty(profilePicUrl))
            {
                // Example URL for GitHub-hosted images
                string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/user/";

                // Combine GitHub base URL with the image file name (e.g., username or unique name)
                profilePicUrl = githubBaseUrl + profilePicUrl; // Assuming the image name is the same as stored in the database.
            }
            else
            {
                // If no profile picture is found, return a default placeholder image URL
                profilePicUrl = " ";
            }

            return new ProfilePicDTO
            {
                ProfilePicture = profilePicUrl // Returning the full URL of the profile picture
            };
        }

        public async Task<UserProfile> GetProfile(int userId)
        {
            try
            {
                // Retrieve the user profile and organization profile based on the user ID
                var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
                var orgprofile = await _context.Organizations.FirstOrDefaultAsync(p => p.UserId == userId);

                // If both profiles exist, return a combined profile
                if (profile != null && orgprofile != null)
                {
                    // Determine the profile picture URL
                    string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/user/";
                    string profilePicUrl = !string.IsNullOrEmpty(profile.ProfilePicture)
                        ? githubBaseUrl + profile.ProfilePicture // Use profile picture from user profile
                        : !string.IsNullOrEmpty(orgprofile.ProfilePicture)
                            ? githubBaseUrl + orgprofile.ProfilePicture // Use profile picture from organization profile if user profile doesn't have one
                            : " "; // Placeholder if neither profile has a picture

                    return new UserProfile
                    {
                        UserId = profile.UserId,
                        FirstName = profile.FirstName ?? orgprofile.Name,
                        LastName = profile.LastName ?? "",
                        Address = profile.Address ?? orgprofile.Address ?? "",
                        DateOfBirth = profile.DateOfBirth,
                        ProfilePicture = profilePicUrl,
                        Email = profile.Email ?? "",
                        Phone = profile.Phone ?? "",
                        Preferences = profile.Preferences ?? "",
                    };
                }

        
                if (profile != null)
                {
                    string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/user/";
                    string profilePicUrl = !string.IsNullOrEmpty(profile.ProfilePicture)
                        ? githubBaseUrl + profile.ProfilePicture // Use profile picture from user profile
                        : " "; // Placeholder if no profile picture

                    return new UserProfile
                    {
                        UserId = profile.UserId,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName ?? "",
                        Address = profile.Address ?? "",
                        DateOfBirth = profile.DateOfBirth,
                        ProfilePicture = profilePicUrl,
                        Email = profile.Email ?? "",
                        Phone = profile.Phone ?? "",
                        Preferences = profile.Preferences ?? ""
                    };
                }

                // If only the organization profile is found, return it
                if (orgprofile != null)
                {
                    string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/user/";
                    string profilePicUrl = !string.IsNullOrEmpty(orgprofile.ProfilePicture)
                        ? githubBaseUrl + orgprofile.ProfilePicture // Use profile picture from organization profile
                        : " "; // Placeholder if no profile picture

                    return new UserProfile
                    {
                        UserId = orgprofile.UserId,
                        FirstName = orgprofile.Name, // Assuming that orgprofile.Name is used as FirstName
                        LastName = "", // Assuming there's no LastName for organizations
                        Address = orgprofile.Address,
                        DateOfBirth = null, // Organizations typically don't have a date of birth
                        ProfilePicture = profilePicUrl,
                        Email = orgprofile.Email ?? "",
                        Phone = orgprofile.Phone ?? "",
                        Preferences = ""
                    };
                }

                // Return null or throw an exception if no profile or organization is found
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving profile for user ID {userId}");
                throw new Exception("An error occurred while retrieving the profile.");
            }
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
                if (updateDto.ProfilePicture != null)
                {
                    profiles.ProfilePicture = await SaveFileAsync(updateDto.ProfilePicture, updateDto.FirstName);
                }
                else if(profiles.ProfilePicture!=null)
                {
                    // If no new picture is provided, retain the existing picture
                    profiles.ProfilePicture = profiles.ProfilePicture ;
                }
                else
                {
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage = $"Please upload profile pic",
                        ErrorCode = 500 // Internal Server Error
                    };
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
                var orgProfiles = await _context.Organizations.ToListAsync();

                var orgProfilesDTO = _mapper.Map<List<OrganizationDTO>>(orgProfiles);

                string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/organization/";

                foreach (var org in orgProfilesDTO)
                {
                    if (!string.IsNullOrEmpty(org.ProfilePicture))
                    {
                        
                        org.ProfilePicture = githubBaseUrl + org.ProfilePicture;  
                    }
                    else
                    {
                        org.ProfilePicture = " "; 
                    }
                }

                return orgProfilesDTO;
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
                var profiles = await _context.Profiles.ToListAsync();

                string githubBaseUrl = "https://raw.githubusercontent.com/niharikaluna11/my-image-repo/main/profilepic/user/";

                foreach (var profile in profiles)
                {
                    if (!string.IsNullOrEmpty(profile.ProfilePicture))
                    {
                        profile.ProfilePicture = githubBaseUrl + profile.ProfilePicture;  
                    }
                    else
                    {
                        profile.ProfilePicture = " ";
                    }
                }

                return _mapper.Map<List<UserProfile>>(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user profiles.");
                throw new Exception("Failed to get user profiles.", ex);
            }
        }

    }
}

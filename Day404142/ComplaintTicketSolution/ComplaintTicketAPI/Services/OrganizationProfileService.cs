using ComplaintTicketAPI.Interfaces;
using System;
using System.Threading.Tasks;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.EmailInterface;
using MimeKit.Encodings;
using ComplaintTicketAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IRepository<int, Organization> _organizationRepo;
        private readonly ILogger<OrganizationProfileService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "ProfilePicture/Organization");
        private readonly ComplaintTicketContext _context;
        public OrganizationProfileService(IRepository<int, Organization> organizationRepo,
            ILogger<OrganizationProfileService> logger,
            ComplaintTicketContext context,
              IEmailSender emailSender)
        {
            _organizationRepo = organizationRepo;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }


        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Generate a unique file name using Guid to avoid overwriting existing files
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            // Save the file to the server
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Check if the file was successfully saved
                if (File.Exists(filePath))
                {
                    return uniqueFileName;
                }
                else
                {
                    _logger.LogError("File not saved.");
                    throw new Exception("Error saving file.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving the file.");
                throw new Exception("Error saving the file.");
            }
        }



        public async Task<Organization> GetOrganizationProfile(int userId)
        {
            try
            {
                var orgProfile = await _context.Organizations.FirstOrDefaultAsync(o => o.UserId == userId);


                return orgProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving organization profile for user ID {userId}");
                throw new Exception("An error occurred while retrieving the organization profile.");
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

        public async Task<Organization> UpdateOrganizationProfile(int userId, OrganizationProfileDTO updateDto)
        {
            try
            {
                var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.UserId == userId);

                if (organization == null)
                {
                    throw new KeyNotFoundException($"Organization with UserId {userId} not found.");
                }

                // Update fields
                organization.Name = updateDto.Name;
                organization.Email = updateDto.Email;
                organization.Phone = updateDto.Phone;
                organization.Address = updateDto.Address;
                organization.Types = updateDto.Types;

                // Handle profile picture saving
                organization.ProfilePicture = await SaveFileAsync(updateDto.ProfilePicture);

                // Send email notification
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

    <p class='greeting'>Dear <strong>{organization.Name}</strong>,</p>
    
    <p class='content'>We are pleased to inform you that your account profile has been successfully updated.</p>
    
    <p class='content footer'>If you have any questions or need further assistance, please do not hesitate to contact us.</p>
    
    <p class='footer'>Best regards,<br/><span class='signature'>TicketSolve Team</span></p>
</body>
</html>";

                    string email = organization.Email;
                    SendMail("Your Account Has Been Created", email, body);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email notification.");
                    throw new Exception("Not able to send email because of invalid mail.");
                }

                // Update organization in the database
                _context.Organizations.Update(organization);
                await _context.SaveChangesAsync();

                return organization;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating organization profile for user ID {userId}: {ex.Message}");
                throw new Exception("An error occurred while updating the organization profile.");
            }
        }


    }
}

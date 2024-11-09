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

namespace ComplaintTicketAPI.Services
{
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IRepository<int, Organization> _organizationRepo;
        private readonly ILogger<OrganizationProfileService> _logger;
        private readonly IEmailSender _emailSender;
        public OrganizationProfileService(IRepository<int, Organization> organizationRepo,
            ILogger<OrganizationProfileService> logger,
              IEmailSender emailSender)
        {
            _organizationRepo = organizationRepo;
            _logger = logger;
            _emailSender = emailSender;
        }

     
        public async Task<Organization> GetOrganizationProfile(int userId)
        {
            try
            {
                return await _organizationRepo.Get(userId);
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
                var organization = await _organizationRepo.Get(userId);
                if (organization == null)
                {
                    return null;
                }

                // Update organization fields with new data from DTO
                organization.Name = updateDto.Name;
                organization.Email = updateDto.Email;
                organization.Phone = updateDto.Phone;
                organization.Address = updateDto.Address;
                organization.Types = updateDto.Types; // assuming Types is included in ProfileUpdateDTO

                try
                {
                    string body = $"Dear {organization.Name},\n\n" +
                          "We are pleased to inform you that your account profile has been successfully updated.\n\n" +
                          "If you have any questions or need further assistance, please do not hesitate to contact us.\n\n" +
                          "Best regards,\n" +
                          "ComplaintTicketApp\n" +
                          "Support Team" +
                          "Niharika Garg";

                    string email = organization.Email;
                    SendMail("Your Account Has Been Created", email, body);
                }
                catch
                {
                    throw new Exception("Not able to send becuase of invalid mail");
                }
                return await _organizationRepo.Update( organization, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating organization profile for user ID {userId}");
                throw new Exception("An error occurred while updating the organization profile.");
            }
        }
    }
}

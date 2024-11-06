using ComplaintTicketAPI.Interfaces;
using System;
using System.Threading.Tasks;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.Extensions.Logging;

namespace ComplaintTicketAPI.Services
{
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IRepository<int, Organization> _organizationRepo;
        private readonly ILogger<OrganizationProfileService> _logger;

        public OrganizationProfileService(IRepository<int, Organization> organizationRepo, ILogger<OrganizationProfileService> logger)
        {
            _organizationRepo = organizationRepo;
            _logger = logger;
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

                // Save changes
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

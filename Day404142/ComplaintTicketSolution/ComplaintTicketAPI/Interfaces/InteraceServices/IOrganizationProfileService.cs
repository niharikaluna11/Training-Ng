using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IOrganizationProfileService
    {

        Task<Organization> GetOrganizationProfile(int userId);
        Task<Organization> UpdateOrganizationProfile(int userId, OrganizationProfileDTO organizationProfileDTO);
    }

}
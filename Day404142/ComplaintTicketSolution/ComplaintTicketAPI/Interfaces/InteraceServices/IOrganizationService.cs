using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IOrganizationService
    {
        Task<OrganizationIdDTO> GetOrganizationByUserIdAsync(int userId);
        Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync();
    }
}

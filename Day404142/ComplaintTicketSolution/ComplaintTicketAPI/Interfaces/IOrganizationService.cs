using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync();
    }
}

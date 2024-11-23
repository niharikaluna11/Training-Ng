using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync();
    }
}

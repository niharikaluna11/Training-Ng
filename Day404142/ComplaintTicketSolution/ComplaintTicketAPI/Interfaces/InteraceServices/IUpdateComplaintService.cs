using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IUpdateComplaintService
    {
        Task<IEnumerable<Complaint>> GetComplaintByOrganizationIdAsync(int orgId);

        Task<bool> UpdateComplaintStatusAsync(UpdateComplaintRequestDTO updateRequest);
    }
}

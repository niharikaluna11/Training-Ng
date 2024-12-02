using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintGetService
    {
        Task<int> GetComplaintCountByOrganizationIdAsync(int orgId);
        Task<IEnumerable<Complaint>> GetComplaintByOrganizationIdAsync(int orgId, int pagenum, int pagesize);
   
    }
}

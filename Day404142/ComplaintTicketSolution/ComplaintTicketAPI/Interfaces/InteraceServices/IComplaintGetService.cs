using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintGetService
    {
        Task<int> GetComplaintCountAsync();
        Task<IEnumerable<Complaint>> GetComplaintsAsync(int pagenum, int pagesize);
        Task<int> GetComplaintCountByOrganizationIdAsync(int orgId);
        Task<IEnumerable<Complaint>> GetComplaintByOrganizationIdAsync(int orgId, int pagenum, int pagesize);
        Task<int> GetComplaintCountByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Complaint>> GetComplaintsByUserIdAsync(int userId, int pagenum, int pagesize);
        Task<int> GetComplaintCountByUserIdAsync(int userId);
        Task<IEnumerable<Complaint>> GetComplaintsByCategoryIdAsync(int categoryId, int pagenum, int pagesize);
    }
}

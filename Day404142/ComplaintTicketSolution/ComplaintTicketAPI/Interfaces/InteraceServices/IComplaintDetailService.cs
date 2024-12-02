using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintDetailService
    {
        Task<string> GetComplaintCategoryAsync(int categoryId);
        Task<List<string>> GetComplaintFilesAsync(int complaintId);
        Task<DateTime?> GetLatestStatusDateAsync(int complaintId);
        Task<ComplaintDetailDto> GetComplaintDetailsAsync(int complaintId);
        Task<User> GetUserDetailsAsync(int userId);
        Task<Organization> GetOrganizationDetailsAsync(int orgId);
        Task<(string Status, string Priority)> GetLatestStatusAsync(int complaintId);
    }
}

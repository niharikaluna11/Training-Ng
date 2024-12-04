using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintDetailService
    {
        Task<string> GetComplaintCategoryAsync(int categoryId);
        Task<List<string>> GetComplaintFilesAsync(int complaintId);
        Task<(string Status, string Priority, DateTime? LatestStatusDate)> GetLatestStatusAsync(int complaintId);
        Task<ComplaintDetailDto> GetComplaintDetailsAsync(int complaintId);
        Task<User> GetUserDetailsAsync(int userId);

        Task<(DateTime? StatusDate, string Status)> GetLatestStatusDateAsync(int complaintId);
        Task<Organization> GetOrganizationDetailsAsync(int orgId);
    }
}

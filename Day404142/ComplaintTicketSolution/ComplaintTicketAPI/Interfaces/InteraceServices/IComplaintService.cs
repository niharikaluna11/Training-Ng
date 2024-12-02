using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintService
    {
        Task<List<Complaint>> GetComplaintsByCategoryId(int categoryId);
        Task<Complaint> CreateComplaint(CreateComplaintRequestDTO complaintDto);
        Task<Complaint> GetComplaint(int id); // Add GetComplaint method

        Task<List<Complaint>> GetAllComplaint();

        Task<ComplaintStatusDTO> TrackComplaintStatus(int complaintId);

    }
}

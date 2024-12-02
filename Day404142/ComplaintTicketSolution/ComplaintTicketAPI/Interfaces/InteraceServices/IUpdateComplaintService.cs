using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IUpdateComplaintService
    {
       
        Task<bool> UpdateComplaintStatusAsync(UpdateComplaintRequestDTO updateRequest);
    }
}

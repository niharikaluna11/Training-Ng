using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IComplaintFileRepository : IRepository<int, ComplaintFile>
    {
        Task AddFiles(IEnumerable<ComplaintFile> files);
       
    }
}

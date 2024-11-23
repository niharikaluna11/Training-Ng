// Create a custom interface that extends the base IRepository
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;

public interface IComplaintRepository : IRepository<int, Complaint>
{
    Task<IEnumerable<Complaint>> GetComplaintsByOrganizationId(int organizationId);
}

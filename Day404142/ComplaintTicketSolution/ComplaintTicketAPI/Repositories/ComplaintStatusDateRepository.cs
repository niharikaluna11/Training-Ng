using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class ComplaintStatusDateRepository : IRepository<int, ComplaintStatusDate>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintStatusDateRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintStatusDate> Add(ComplaintStatusDate entity)
        {
            await _context.ComplaintStatusDates.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintStatusDate> Get(int id)
        {
            return await _context.ComplaintStatusDates
                .Include(csd => csd.Complaint)  // Include related entities if necessary
                .Include(csd => csd.ComplaintStatus)
                .FirstOrDefaultAsync(csd => csd.ComplaintId == id);
        }

        public async Task<IEnumerable<ComplaintStatusDate>> GetAll()
        {
            return await _context.ComplaintStatusDates
                .Include(csd => csd.Complaint)  // Include related entities if necessary
                .Include(csd => csd.ComplaintStatus)
                .ToListAsync();
        }

        public async Task<ComplaintStatusDate> Update(ComplaintStatusDate entity)
        {
            _context.ComplaintStatusDates.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var complaintStatusDate = await _context.ComplaintStatusDates.FindAsync(id);
            if (complaintStatusDate != null)
            {
                _context.ComplaintStatusDates.Remove(complaintStatusDate);
                await _context.SaveChangesAsync();
            }
        }

        public Task<ComplaintStatusDate> Update(ComplaintStatusDate entity, int key)
        {
            throw new NotImplementedException();

        }

        Task<ComplaintStatusDate> IRepository<int, ComplaintStatusDate>.Delete(int key)
        {
            throw new NotImplementedException();
        }
    }
}

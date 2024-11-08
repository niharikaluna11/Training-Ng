using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class ComplaintStatusRepository : IRepository<int,ComplaintStatus>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintStatusRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintStatus> Add(ComplaintStatus entity)
        {
            _context.ComplaintStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintStatus> Update(ComplaintStatus entity, int key)
        {
            var existing = await _context.ComplaintStatuses.FindAsync(key);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintStatus> Delete(int key)
        {
            var entity = await _context.ComplaintStatuses.FindAsync(key);
            if (entity == null) return null;
            _context.ComplaintStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintStatus> Get(int key)
        {
            return await _context.ComplaintStatuses.FindAsync(key);
        }

        public async Task<IEnumerable<ComplaintStatus>> GetAll()
        {
            return await _context.ComplaintStatuses.ToListAsync();
        }
    }
}

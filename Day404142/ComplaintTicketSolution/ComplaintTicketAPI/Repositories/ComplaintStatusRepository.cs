using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    // all done 
    public class ComplaintStatusRepository : IRepository<int, ComplaintStatus>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintStatusRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintStatus> Add(ComplaintStatus entity)
        {
            try
            {
                _context.ComplaintStatuses.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to add complaint status"); }

        }

        public async Task<ComplaintStatus> Update(ComplaintStatus entity, int key)
        {
            try
            {
                var existing = await _context.ComplaintStatuses.FindAsync(key);
                if (existing == null) return null;
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to update complaint status"); }

        }

        public async Task<ComplaintStatus> Delete(int key)
        {
            try
            {
                var entity = await _context.ComplaintStatuses.FindAsync(key);
                if (entity == null) return null;
                _context.ComplaintStatuses.Remove(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch { throw new Exception("not able to update complaint status"); }
        }

        public async Task<ComplaintStatus> Get(int key)
        {
            try
            {
                return await _context.ComplaintStatuses.FindAsync(key);
            }
            catch { throw new Exception("not able to get complaint status"); }
        }

        public async Task<IEnumerable<ComplaintStatus>> GetAll()
        {
            try
            {
                return await _context.ComplaintStatuses.ToListAsync();
            }
            catch { throw new Exception("not able to get complaint status"); }

        }
    }
}

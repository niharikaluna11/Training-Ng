using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class ComplaintFileRepository : IRepository<int, ComplaintFile>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintFileRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintFile> Add(ComplaintFile entity)
        {
            _context.ComplaintFiles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintFile> Update(ComplaintFile entity, int key)
        {
            var existing = await _context.ComplaintFiles.FindAsync(key);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintFile> Delete(int key)
        {
            var entity = await _context.ComplaintFiles.FindAsync(key);
            if (entity == null) return null;
            _context.ComplaintFiles.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintFile> Get(int key)
        {
            return await _context.ComplaintFiles.FindAsync(key);
        }

        public async Task<IEnumerable<ComplaintFile>> GetAll()
        {
            return await _context.ComplaintFiles.ToListAsync();
        }
    }

}

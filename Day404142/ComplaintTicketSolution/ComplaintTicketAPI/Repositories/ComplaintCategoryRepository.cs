using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class ComplaintCategoryRepository : IRepository<int, ComplaintCategory>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintCategoryRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintCategory> Add(ComplaintCategory entity)
        {
            _context.ComplaintCategories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintCategory> Update(ComplaintCategory entity, int key)
        {
            var existing = await _context.ComplaintCategories.FindAsync(key);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintCategory> Delete(int key)
        {
            var entity = await _context.ComplaintCategories.FindAsync(key);
            if (entity == null) return null;
            _context.ComplaintCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ComplaintCategory> Get(int key)
        {
            return await _context.ComplaintCategories.FindAsync(key);
        }

        public async Task<IEnumerable<ComplaintCategory>> GetAll()
        {
            return await _context.ComplaintCategories.ToListAsync();
        }
    }

}

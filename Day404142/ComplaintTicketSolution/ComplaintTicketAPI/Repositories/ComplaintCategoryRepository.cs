using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    //all done
    public class ComplaintCategoryRepository : IRepository<int, ComplaintCategory>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintCategoryRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintCategory> Add(ComplaintCategory entity)
        {
            try
            {
                _context.ComplaintCategories.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not abble to add complaint category entity"); }

        }

        public async Task<ComplaintCategory> Update(ComplaintCategory entity, int key)
        {
            try
            {
                var existing = await _context.ComplaintCategories.FindAsync(key);
                if (existing == null) return null;
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not abble to update complaint category entity"); }
           
        }

        public async Task<ComplaintCategory> Delete(int key)
        {
            try
            {
                var entity = await _context.ComplaintCategories.FindAsync(key);
                if (entity == null) return null;
                _context.ComplaintCategories.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not abble to delete complaint category entity"); }
         
        }

        public async Task<ComplaintCategory> Get(int key)
        {
            try
            {
                return await _context.ComplaintCategories.FindAsync(key);
            }
            catch { throw new Exception("not abble to get complaint category entity"); }
           
        }

        public async Task<IEnumerable<ComplaintCategory>> GetAll()
        {
            try
            {
                return await _context.ComplaintCategories.ToListAsync();
            }
            catch { throw new Exception("not abble to get complaint category entity"); }
            
        }
    }

}

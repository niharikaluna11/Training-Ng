using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComplaintTicketAPI.Repositories
{
    //alll done
    public class ComplaintFileRepository : IComplaintFileRepository
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintFileRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task AddFiles(IEnumerable<ComplaintFile> files)
        {
            await _context.ComplaintFiles.AddRangeAsync(files);
            await _context.SaveChangesAsync();
        }
        public async Task<ComplaintFile> Add(ComplaintFile entity)
        {
            try
            {

                _context.ComplaintFiles.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to add entity complaint file"); }

        }

        public async Task<ComplaintFile> Update(ComplaintFile entity, int key)
        {
            try
            {

                var existing = await _context.ComplaintFiles.FindAsync(key);
                if (existing == null) return null;
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to update entity complaint file"); }
           
        }

        public async Task<ComplaintFile> Delete(int key)
        {

            try
            {

                var entity = await _context.ComplaintFiles.FindAsync(key);
                if (entity == null) return null;
                _context.ComplaintFiles.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to delete entity complaint file"); }

          
        }

        public async Task<ComplaintFile> Get(int key)
        {
            try
            {


                return await _context.ComplaintFiles.FindAsync(key);
            }
            catch { throw new Exception("not able to get entity complaint file"); }


        }

        public async Task<IEnumerable<ComplaintFile>> GetAll()
        {
            try
            {
                return await _context.ComplaintFiles.ToListAsync();
            }
            catch { throw new Exception("not able to get entity complaint file"); }


           
        }
    }

}

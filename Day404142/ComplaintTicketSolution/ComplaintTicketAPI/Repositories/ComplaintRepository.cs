using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    //all done
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintRepository(ComplaintTicketContext context)
        {
            _context = context;
        }


        // Add Complaint
        public async Task<Complaint> Add(Complaint entity)
        {
            try
            {
                _context.Complaints.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to add entity complaint"); }
        }

        // Get All Complaints
        public async Task<IEnumerable<Complaint>> GetAll()
        {
            try
            {
                return await _context.Complaints.ToListAsync();
            }
            catch { throw new Exception("empty complaint"); }

        }

        public async Task<Complaint> Get(int key)
        {
            try
            {

                return await _context.Complaints
                                     .Include(c => c.User)              // Include related user data
                                     .Include(c => c.Category)           // Include related category data
                                     .FirstOrDefaultAsync(c => c.Id == key); // Get the complaint by ID
            }
            catch { throw new Exception("empty complaint"); }


        }

        // Update Complaint
        public async Task<Complaint> Update(Complaint entity, int key)
        {

            try
            {

                var existing = await _context.Complaints.FindAsync(key);
                if (existing == null) return null;
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to update complaint"); }

           
        }
        public async Task<IEnumerable<Complaint>> GetComplaintsByOrganizationId(int organizationId)
        {
            return await _context.Complaints
                                 .Where(c => c.OrganizationId == organizationId)
                                 .ToListAsync();
        }


        // Delete Complaint
        public async Task<Complaint> Delete(int key)
        {
            try
            {

                var entity = await _context.Complaints.FindAsync(key);
                if (entity == null) return null;
                _context.Complaints.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to delete complaint"); }
            
        }


    }




}

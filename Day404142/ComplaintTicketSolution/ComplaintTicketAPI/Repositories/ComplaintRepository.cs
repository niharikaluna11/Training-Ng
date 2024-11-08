using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class ComplaintRepository : IRepository<int, Complaint>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintRepository(ComplaintTicketContext context)
        {
            _context = context;
        }


        // Add Complaint
        public async Task<Complaint> Add(Complaint entity)
        {
            _context.Complaints.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Get All Complaints
        public async Task<IEnumerable<Complaint>> GetAll()
        {
            return await _context.Complaints.ToListAsync();
        }

        public async Task<Complaint> Get(int key)
        {
       
            return await _context.Complaints
                                 .Include(c => c.User)              // Include related user data
                                 .Include(c => c.Category)           // Include related category data
                                 .FirstOrDefaultAsync(c => c.Id == key); // Get the complaint by ID
        }

        // Update Complaint
        public async Task<Complaint> Update(Complaint entity, int key)
        {
            var existing = await _context.Complaints.FindAsync(key);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Delete Complaint
        public async Task<Complaint> Delete(int key)
        {
            var entity = await _context.Complaints.FindAsync(key);
            if (entity == null) return null;
            _context.Complaints.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

       
    }




}

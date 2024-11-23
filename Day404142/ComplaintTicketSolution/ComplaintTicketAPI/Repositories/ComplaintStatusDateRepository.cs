using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    //all done
    public class ComplaintStatusDateRepository : IRepository<int, ComplaintStatusDate>
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintStatusDateRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<ComplaintStatusDate> Add(ComplaintStatusDate entity)
        {
            try
            {
                await _context.ComplaintStatusDates.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to add status date part"); }

        }

        public async Task<ComplaintStatusDate> Get(int id)
        {

            try
            {
                return await _context.ComplaintStatusDates
               .Include(csd => csd.Complaint)  // Include related entities if necessary
               .Include(csd => csd.ComplaintStatus)
               .FirstOrDefaultAsync(csd => csd.ComplaintId == id);
            }
            catch { throw new Exception("not able to get status date part"); }

        }

        public async Task<IEnumerable<ComplaintStatusDate>> GetAll()
        {

            try
            {
                return await _context.ComplaintStatusDates
                .Include(csd => csd.Complaint)  // Include related entities if necessary
                .Include(csd => csd.ComplaintStatus)
                .ToListAsync();
            }
            catch { throw new Exception("not able to get status date part"); }


        }

        public async Task<ComplaintStatusDate> Update(ComplaintStatusDate entity)
        {

            try
            {

                _context.ComplaintStatusDates.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch { throw new Exception("not able to get status date part"); }

        }

        public async Task Delete(int id)
        {
            try
            {

                var complaintStatusDate = await _context.ComplaintStatusDates.FindAsync(id);
                if (complaintStatusDate != null)
                {
                    _context.ComplaintStatusDates.Remove(complaintStatusDate);
                    await _context.SaveChangesAsync();
                }
            }
            catch { throw new Exception("not able to get status date part"); }

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

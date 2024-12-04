using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InterfaceRepositories;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Repositories
{
    public class UserHelpRepository : IUserHelpRepository
    {

        private readonly ComplaintTicketContext _context;

        public UserHelpRepository(ComplaintTicketContext context)
        {
            _context = context;
        }
        // Get all user queries
        public async Task<IEnumerable<UserHelp>> GetAllQueriesAsync()
        {
            return await _context.UserHelps.ToListAsync();
        }

        // Get a specific query by its ID
        public async Task<UserHelp> GetQueryByEmailAsync(string email)
        {
            return await _context.UserHelps
                                 .FirstOrDefaultAsync(u => u.email == email);
        }


        // Add a new query to the database
        public async Task AddQueryAsync(UserHelp userHelp)
        {
            await _context.UserHelps.AddAsync(userHelp);
            await _context.SaveChangesAsync();
        }

        // Update an existing query
        public async Task UpdateQueryAsync(UserHelp userHelp)
        {
            _context.UserHelps.Update(userHelp);
            await _context.SaveChangesAsync();
        }

        public async Task<UserHelp> GetQueryByEmailifAsync(string email)
        {
            var result = await _context.UserHelps
              .FirstOrDefaultAsync(u => u.email == email && !u.IsResponded); // Get the first unresponded record
            return result;

        }
    }
}

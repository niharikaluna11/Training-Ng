using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ComplaintTicketAPI.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly ComplaintTicketContext _context;

        public UserRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            if (await UserExists(entity.Username))
            {
                throw new InvalidOperationException("User already exists. Please log in.");
            }

            try
            {
                var addedUser = await _context.Users.AddAsync(entity);
                await _context.SaveChangesAsync();
                return addedUser.Entity; 
            }
            catch (DbUpdateException)
            {
                throw new CouldNotAddException("Failed to add user due to database constraints.");
            }
            catch (Exception ex)
            {
                throw new CouldNotAddException("An unexpected error occurred while adding the user.", ex);
            }
        }

        public async Task<User> Delete(string key)
        {
            var user = await Get(key);
            if (user == null)
            {
                throw new CouldNotDeleteException("User not found.");
            }

            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new CouldNotDeleteException("An error occurred while deleting the user.", ex);
            }
        }

        public async Task<User> Get(string key)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == key);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<User> Update(User entity, string key)
        {
            var user = await Get(key);
            if (user == null)
            {
                throw new CouldNotUpdateException("User not found.");
            }

            // Update only relevant fields
            user.Username = entity.Username;
            user.Password = entity.Password;
            user.Roles = entity.Roles;

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException)
            {
                throw new CouldNotUpdateException("An error occurred while updating the user.");
            }
            catch (Exception ex)
            {
                throw new CouldNotUpdateException("An unexpected error occurred while updating the user.", ex);
            }
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}

using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;

// all done 

namespace ComplaintTicketAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ComplaintTicketContext _context;

        public UserRepository(ComplaintTicketContext context)
        {
            _context = context;
        }


        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByResetToken(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == token);
        }

        public async Task<User> GetByUsernameOrEmail(string UsernameorEmail)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == UsernameorEmail || u.Email == UsernameorEmail);
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
                throw new Exception("Failed to add user due to database constraints.");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the user.", ex);
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
                user.IsDeleted = true;
                user.PStatus = PersonStatus.Deactivated;
                
                await _context.SaveChangesAsync();

                return user;  
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
        public async Task<User> Reactivate(string key)
        {
            var user = await Get(key);
            if (user == null || !user.IsDeleted)
            {
                throw new CouldNotDeleteException("User is not soft deleted.");
            }

            try
            {
                // Reactivate the user (mark as not deleted)
                user.IsDeleted = false;
                user.PStatus = PersonStatus.Activated;

                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reactivating the user.", ex);
            }
        }


        public async Task<User> Get(string key)
        {
            try
            {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == key);
            }
            catch (Exception ex)
            {
                throw new CouldNotUpdateException("User not found.");
            }

        }

        public async Task<IEnumerable<User>> GetAll()
        {

            try
            {
                return await _context.Users
                                 .AsNoTracking()
                                 .ToListAsync();
            }
            catch (Exception ex)
            {

                throw new CouldNotUpdateException("User not found.");
            }

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
                throw new Exception("An unexpected error occurred while updating the user.", ex);
            }
        }
        

        public async Task<bool> UserExists(string username)
        {
           
            try
            {
                return await _context.Users.AnyAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while checking username uniqueness.", ex);
            }

        }

        public async Task<bool> EmailExists(string email)
        {

            try
            {
                return await _context.Users.AnyAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("An error occurred while checking email uniquenness", ex);
            }

        }


    }
}

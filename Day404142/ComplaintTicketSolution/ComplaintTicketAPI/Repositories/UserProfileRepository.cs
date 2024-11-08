using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Context;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComplaintTicketAPI.Repositories
{
    public class UserProfileRepository : IRepository<int, UserProfile>
    {
        private readonly ComplaintTicketContext _context;

        public UserProfileRepository(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> Add(UserProfile entity)
        {
            try
            {
                var profile = await _context.Profiles.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new InvalidOperationException("Some error");
            }
            
        }

        public Task<UserProfile> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> Get(int userId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<UserProfile> Update(UserProfile entity, int userId)
        {
            // Check if the profile exists
            var existingProfile = await Get(userId);

            if (existingProfile == null)
            {
                // If no profile exists, create a new one
                var newProfile = new UserProfile
                {
                    UserId = userId, // Set the userId for the new profile
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Address = entity.Address,
                    DateOfBirth = entity.DateOfBirth,
                    Email = entity.Email,
                    Phone = entity.Phone,
                    Preferences = entity.Preferences
                };

                // Add the new profile to the context and save changes
                _context.Profiles.Add(newProfile);
                await _context.SaveChangesAsync();
                return newProfile; // Return the newly created profile
            }

            // If the profile exists, update its properties
            existingProfile.FirstName = entity.FirstName;
            existingProfile.LastName = entity.LastName;
            existingProfile.Address = entity.Address;
            existingProfile.DateOfBirth = entity.DateOfBirth;
            existingProfile.Email = entity.Email;
            existingProfile.Phone = entity.Phone;
            existingProfile.Preferences = entity.Preferences;

            // Save changes to the existing profile
            await _context.SaveChangesAsync();
            return existingProfile; // Return the updated profile
        }



    }
}

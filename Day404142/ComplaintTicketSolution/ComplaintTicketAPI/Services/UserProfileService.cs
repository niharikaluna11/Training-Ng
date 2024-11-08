using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ComplaintTicketContext _context;

        public UserProfileService(ComplaintTicketContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetProfile(int userId)
        {
            // Retrieve the profile from the database by userId
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            return profile; // Return the found profile
        }


        public async Task<UserProfile> UpdateProfile(int userId, ProfileUpdateDTO updateDto)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile != null)
            {


                // Update profile properties
                profile.FirstName = updateDto.FirstName;
                profile.LastName = updateDto.LastName;
                profile.Address = updateDto.Address;
                profile.DateOfBirth = updateDto.DateOfBirth;
                profile.Email = updateDto.Email;
                profile.Phone = updateDto.Phone;
                profile.Preferences = updateDto.Preferences;

                await _context.SaveChangesAsync();
            }
            return profile; // Return the updated profile
        }
    }
}

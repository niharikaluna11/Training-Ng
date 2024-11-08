using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfile> UpdateProfile(int userId, ProfileUpdateDTO updateDto);

        Task<UserProfile> GetProfile(int userId);
    }
}

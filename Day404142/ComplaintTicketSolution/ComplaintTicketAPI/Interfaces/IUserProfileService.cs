using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IUserProfileService
    {
        Task<Profile> UpdateProfile(int userId, ProfileUpdateDTO updateDto);

        Task<Profile> GetProfile(int userId);
    }
}

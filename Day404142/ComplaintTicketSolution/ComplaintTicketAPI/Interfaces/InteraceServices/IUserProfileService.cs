using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IUserProfileService
    {
        Task<BaseResponseDTO> UpdateProfile(int userId, ProfileUpdateDTO updateDto);
        Task<ProfilePicDTO> GetProfilePic(string username);
        Task<UserProfile> GetProfile(int userId);

        Task<List<OrganizationDTO>> GetALLOrgProfile();
        Task<List<UserProfile>> GetALLUserProfile();
    }
}

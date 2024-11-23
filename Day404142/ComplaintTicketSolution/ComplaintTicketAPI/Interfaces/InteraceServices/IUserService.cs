using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IUserService

    {
        Task<User> ReactivateUserAsync(string key);
        Task<User> SoftDeleteUserAsync(string key);
        Task<BaseResponseDTO> SendRegistrationOtp(string email);
        Task<BaseResponseDTO> VerifyRegistrationOtp(string email, string otp);
        Task<BaseResponseDTO> Authenticate(LoginRequestDTO loginUser);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<BaseResponseDTO> Register(RegisterUserDto registerUser);
    }
}

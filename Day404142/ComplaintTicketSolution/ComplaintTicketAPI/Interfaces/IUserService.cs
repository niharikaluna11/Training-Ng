using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponseDTO> Authenticate(LoginRequestDTO loginUser);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<BaseResponseDTO> Register(RegisterUserDto registerUser);
    }
}

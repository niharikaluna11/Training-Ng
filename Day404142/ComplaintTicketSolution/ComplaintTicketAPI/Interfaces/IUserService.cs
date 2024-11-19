using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IUserService
    {
        Task<SuccessResponseDTO<LoginResponseDTO>> Authenticate(LoginRequestDTO loginUser);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<SuccessResponseDTO<LoginResponseDTO>> Register(RegisterUserDto registerUser);
    }
}

using EFFirstAPI.Models.DTO;

namespace EFFirstAPI.Interfaces
{
    public interface IUserService
    {

        public Task<LoginResponseDTO> Autheticate(LoginResponseDTO loginUser);
        public Task<LoginResponseDTO> Register(UserDTO registerUser);

    }
}

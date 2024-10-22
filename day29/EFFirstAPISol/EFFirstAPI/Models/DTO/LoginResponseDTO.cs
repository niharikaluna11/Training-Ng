using EFFirstAPI.Models;

namespace EFFirstAPI.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}


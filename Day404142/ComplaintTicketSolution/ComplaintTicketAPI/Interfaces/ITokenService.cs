using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}


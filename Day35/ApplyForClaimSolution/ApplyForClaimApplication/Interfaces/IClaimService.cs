

using ApplyForClaimApplication.Models;
using ApplyForClaimApplication.Models.DTO;

namespace ApplyForClaimApplication.Interfaces
{
    public interface IClaimService
    {

        Task<int> CreateClaim(ClaimRequestDTO claim);
        Task<IEnumerable<ClaimData>> GetAllClaims();
        Task<ClaimData> GetClaim(int id);
        Task<ClaimData> UpdateClaimStatus(string status, int id);

    }

}

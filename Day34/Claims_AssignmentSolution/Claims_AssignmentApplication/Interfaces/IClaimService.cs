using Claims_AssignmentApplication.Models;
using Claims_AssignmentApplication.Models.DTOs;

namespace Claims_AssignmentApplication.Interfaces
{
    public interface IClaimService
    {

        Task<int> CreateClaim(ClaimRequestDTO claim);
        Task<IEnumerable<Claims>> GetAllClaims();
        Task<Claims> GetClaim(int id);
        Task<Claims> UpdateClaimStatus(string status, int id);

    }

}

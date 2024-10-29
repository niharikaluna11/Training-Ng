using ReportClaimApplication.Models.DTO;

namespace ReportClaimApplication.Interfaces
{
    public interface IClaimService
    {

        Task<ClaimResponseDTO> CreateClaimAsync(ClaimRequestDTO claimRequest);
        Task<ClaimResponseDTO> GetClaimByIdAsync(int claimId);
        Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync();

    }

}

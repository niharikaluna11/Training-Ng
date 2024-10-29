using AutoMapper;
using ReportClaimApplication.Interfaces;
using ReportClaimApplication.Models.DTO;
using ReportClaimApplication.Models;
using System.Security.Claims;

namespace ReportClaimApplication.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<Claims> _claimRepository; // Use the generic repository
        private readonly IMapper _mapper;

        public ClaimService(IRepository<Claims> claimRepository, IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        // Method to create a new claim
        public async Task<ClaimResponseDTO> CreateClaimAsync(ClaimRequestDTO claimRequest)
        {
            var claim = _mapper.Map<Claims>(claimRequest); // Map DTO to Claim model
            await _claimRepository.AddAsync(claim); // Call the repository to add the claim
            return _mapper.Map<ClaimResponseDTO>(claim); // Map the claim back to ClaimResponseDTO for response
        }

        // Method to get a claim by ID
        public async Task<ClaimResponseDTO> GetClaimByIdAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId); // Get the claim from the repository
            return _mapper.Map<ClaimResponseDTO>(claim); // Map to DTO
        }

        // Method to get all claims
        public async Task<IEnumerable<ClaimResponseDTO>> GetAllClaimsAsync()
        {
            var claims = await _claimRepository.GetAllAsync(); // Get all claims from repository
            return _mapper.Map<IEnumerable<ClaimResponseDTO>>(claims); // Map to a list of DTOs
        }
    }
}

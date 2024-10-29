using AutoMapper;
using Claims_AssignmentApplication.Exceptions;
using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models;
using Claims_AssignmentApplication.Models.DTOs;
using ReportClaimApplication.Exceptions;

namespace Claims_AssignmentApplication.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<int, Claims> _claimRepo;
        private readonly IMapper _mapper;

        public ClaimService(IRepository<int, Claims> claimRepository, IMapper mapper)
        {
            _claimRepo = claimRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateClaim(ClaimRequestDTO claim)
        {
            //Claims newClaim = _mapper.Map<Claims>(claim);
            Claims newClaim = new Claims
            {
                PolicyNumber = claim.PolicyNumber,
                TypeOfClaim = claim.ClaimType,
                DateOfIncident = claim.DateOfIncident,
                ClaimantName = claim.ClaimantName,
                ClaimantPhone = claim.PhoneNumber,
                ClaimantEmail = claim.Email,
                ClaimStatus="pending"


            };

            var addedClaim = await _claimRepo.AddAsync(newClaim);
            return addedClaim.ClaimId;
        }

        public async Task<IEnumerable<Claims>> GetAllClaims()
        {
            try
            {
                var claims = await _claimRepo.GetAllAsync();
                return claims;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Claims");
            }
        }

        public async Task<Claims> GetClaim(int id)
        {
            try
            {
                var claim = await _claimRepo.GetByIdAsync(id);
                if (claim != null)
                {
                    return claim;
                }
                throw new NotFoundException("Claim");
            }
            catch (Exception)
            {
                throw new NotFoundException("Claim");
            }
        }

        public async Task<Claims> UpdateClaimStatus(string status, int id)
        {
            var claim = await _claimRepo.GetByIdAsync(id);
            if (claim != null)
            {
                claim.ClaimStatus = status;
                await _claimRepo.UpdateAsync(claim);
                return claim;
            }
            throw new NotUpdateException("Claim status update failed");
        }
    }
}

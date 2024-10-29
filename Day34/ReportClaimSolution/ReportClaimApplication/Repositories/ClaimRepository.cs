using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Context;
using ReportClaimApplication.Models;
using ReportClaimApplication.Exceptions;
using ReportClaimApplication.Interfaces;

namespace ReportClaimApplication.Repositories
{
    public class ClaimRepository : IRepository<Claims>
    {
        private readonly ClaimDbContext _context;

        // Constructor with context
        public ClaimRepository(ClaimDbContext context)
        {
            _context = context;
        }

        // Add a new claim
        public async Task<Claims> AddAsync(Claims entity)
        {
            try
            {
                await _context.Claims.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Claim");
            }
        }

        // Delete a claim by ID
        public async Task<Claims> DeleteAsync(int id)
        {
            var claim = await GetByIdAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync();
                return claim;
            }
            throw new NotFoundException("Claim for deletion");
        }

        // Get a claim by ID
        public async Task<Claims> GetByIdAsync(int id)
        {
            var claim = await _context.Claims
                .Include(c => c.Documents) // Include documents if needed
                .FirstOrDefaultAsync(c => c.ClaimId == id);
            return claim;
        }

        // Get all claims
        public async Task<IEnumerable<Claims>> GetAllAsync()
        {
            var claims = await _context.Claims.ToListAsync();
            if (claims.Count == 0)
            {
                throw new CollectionEmptyException("Claims");
            }
            return claims;
        }

        // Update an existing claim
        public async Task UpdateAsync(Claims entity)
        {
            var existingClaim = await GetByIdAsync(entity.ClaimId);
            if (existingClaim == null)
            {
                throw new NotFoundException("Claim not found for update");
            }

            try
            {
                _context.Claims.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new CouldNotUpdateException("Claim");
            }
        }
    }
}

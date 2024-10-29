using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Context;
using ReportClaimApplication.Models;
using ReportClaimApplication.Exceptions;
using ReportClaimApplication.Interfaces;

namespace ReportClaimApplication.Repositories
{
    public class PolicyRepository : IRepository<Policy>
    {
        private readonly ClaimDbContext _context;

        // Constructor with context
        public PolicyRepository(ClaimDbContext context)
        {
            _context = context;
        }

        // Add a new policy
        public async Task<Policy> AddAsync(Policy entity)
        {
            try
            {
                await _context.Policies.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Policy");
            }
        }

        // Delete a policy by ID
        public async Task<Policy> DeleteAsync(int id)
        {
            var policy = await GetByIdAsync(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
                return policy;
            }
            throw new NotFoundException("Policy for deletion");
        }

        // Get a policy by ID
        public async Task<Policy> GetByIdAsync(int id)
        {
            return await _context.Policies.FirstOrDefaultAsync(p => p.PolicyId == id);
        }

        // Get all policies
        public async Task<IEnumerable<Policy>> GetAllAsync()
        {
            var policies = await _context.Policies.ToListAsync();
            if (policies.Count == 0)
            {
                throw new CollectionEmptyException("Policies");
            }
            return policies;
        }

        // Update an existing policy
        public async Task UpdateAsync(Policy entity)
        {
            var existingPolicy = await GetByIdAsync(entity.PolicyId);
            if (existingPolicy == null)
            {
                throw new NotFoundException("Policy not found for update");
            }

            try
            {
                _context.Policies.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new CouldNotUpdateException("Policy");
            }
        }
    }
}

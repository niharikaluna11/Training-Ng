using Claims_AssignmentApplication.Context;
using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models;
using Microsoft.EntityFrameworkCore;
using ReportClaimApplication.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claims_AssignmentApplication.Repositories
{
    public class ClaimRepository : IRepository<int, Claims>
    {
        private readonly ClaimContext _context;

        public ClaimRepository(ClaimContext context)
        {
            _context = context;
        }

        public async Task<Claims> AddAsync(Claims entity)
        {
            try
            {
                await _context.Claims.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity; // Return the added entity
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Claim");
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var claim = await GetByIdAsync(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                await _context.SaveChangesAsync(); // Ensure changes are saved
                return true; // Return true if deletion is successful
            }
            throw new NotFoundException("Claim for deletion");
        }

        public async Task<Claims> GetByIdAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id); // Use FindAsync for direct lookup
            if (claim == null)
            {
                throw new NotFoundException("Claim");
            }
            return claim;
        }

        public async Task<IEnumerable<Claims>> GetAllAsync()
        {
            var claims = await _context.Claims.ToListAsync();
            if (!claims.Any())
            {
                throw new CollectionEmptyException("Claims");
            }
            return claims;
        }

        public async Task UpdateAsync(Claims entity)
        {
            try
            {
                _context.Claims.Update(entity);
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception)
            {
                throw new CouldNotUpdateException("Claim");
            }
        }
    }
}

using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintTicketAPI.Context;
using Microsoft.Extensions.Logging;
using ComplaintTicketAPI.Interfaces.InteraceServices;

namespace ComplaintTicketAPI.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ComplaintTicketContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationService> _logger;


     

        public OrganizationService(ComplaintTicketContext context, IMapper mapper, ILogger<OrganizationService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrganizationIdDTO> GetOrganizationByUserIdAsync(int userId)
        {
            try
            {

                var userWithOrg = await _context.Organizations
                  // Assuming there is a navigation property for Organization in the User entity
                  .FirstOrDefaultAsync(u => u.UserId == userId);
                // Check if the user exists
                if (userWithOrg == null)
                {
                    throw new KeyNotFoundException($"No user found with ID {userId}");
                }

                // Check if the organization exists
                if (userWithOrg == null)
                {
                    throw new KeyNotFoundException($"No organization found for user ID {userId}");
                }

                // Map the organization data to DTO
                return _mapper.Map<OrganizationIdDTO>(userWithOrg);
            }
            catch (DbUpdateException dbEx)
            {
                // Log database update errors
                _logger.LogError(dbEx, $"Error occurred while fetching organization for user ID {userId}.");
                throw new Exception("An error occurred while retrieving the organization.", dbEx);
            }
            catch (Exception ex)
            {
                // Log general errors
                _logger.LogError(ex, $"An unexpected error occurred while fetching organization for user ID {userId}.");
                throw new Exception("An unexpected error occurred while retrieving the organization.", ex);
            }
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync()
        {
            try
            {
                // Fetch all organizations
                var organizations = await _context.Organizations.ToListAsync();

                // Map the organization data to DTOs
                return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
            }
            catch (DbUpdateException dbEx)
            {
                // Log database update errors
                _logger.LogError(dbEx, "Error occurred while fetching organizations from the database.");
                throw new Exception("An error occurred while retrieving the organizations.", dbEx);
            }
            catch (Exception ex)
            {
                // Log general errors
                _logger.LogError(ex, "An unexpected error occurred while fetching organizations.");
                throw new Exception("An unexpected error occurred while retrieving the organizations.", ex);
            }
        }
    }
}

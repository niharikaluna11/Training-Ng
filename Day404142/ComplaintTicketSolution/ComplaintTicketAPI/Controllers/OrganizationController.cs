using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using ComplaintTicketAPI.Services;
using Microsoft.AspNetCore.Cors;
using ComplaintTicketAPI.Interfaces.InteraceServices;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]

    // Controller for managing organizations, users, and complaints
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;

        // Constructor to inject services
        public OrganizationController(IOrganizationService organizationService, IUserService userService)
        {
            _organizationService = organizationService;
            _userService = userService;
        }

        [HttpGet("GetOrgByUserID")]
        [Authorize]
        public async Task<IActionResult> GetOrganizationByUserId(int userId)
        {
            try
            {
                var organization = await _organizationService.GetOrganizationByUserIdAsync(userId);

                if (organization == null)
                {
                    return NotFound($"No organization found for user ID {userId}.");
                }

                return Ok(organization);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the error (if a logging mechanism is in place)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a list of available roles for users
        [HttpGet("Roles/Available")]
        [Authorize]
        public IActionResult GetAvailableRoles()
        {
            var roles = Enum.GetValues(typeof(Role));
            var roleList = new List<string>();

            foreach (var role in roles)
            {
                roleList.Add(role.ToString());
            }

            return Ok(roleList);
        }

        // Get a list of available organization types
        [HttpGet("Organizations/AvailableTypes")]
        [Authorize]
        public IActionResult GetAvailableOrganizationTypes()
        {
            var types = Enum.GetValues(typeof(Models.Type));
            var typeList = new List<string>();

            foreach (var type in types)
            {
                typeList.Add(type.ToString());
            }

            return Ok(typeList);
        }

        // Get a list of all organizations
        [HttpGet("Organizations/All")]
        [Authorize]
        public async Task<IActionResult> GetAllOrganizations()
        {
            var organizations = await _organizationService.GetAllOrganizationsAsync();

            if (organizations == null )
            {
                return NotFound("No organizations found.");
            }

            return Ok(organizations);
        }

        [HttpGet("Users/All")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();

                if (!users.Any())
                {
                    return NotFound("No users found.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while fetching users.");
                return StatusCode(500, "Internal server error while fetching users.");
            }
        }



        [HttpGet("Complaints/AvailablePriorities")]
        public IActionResult GetAvailablePriorityLevels()
        {
            var priorities = Enum.GetValues(typeof(Priority));
            var priorityList = new List<string>();

            foreach (var priority in priorities)
            {
                priorityList.Add(priority.ToString());
            }

            return Ok(priorityList);
        }
    }
}

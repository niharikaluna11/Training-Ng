using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IOrganizationProfileService _organizationProfileService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IUserProfileService userProfileService,
                                 IOrganizationProfileService organizationProfileService,
                                 ILogger<ProfileController> logger)
        {
            _userProfileService = userProfileService;
            _organizationProfileService = organizationProfileService;
            _logger = logger;
        }

        // GET: api/profile/{userId}
        [HttpGet("Get-profile/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetProfile(int userId)
        {
            try
            {
                var userProfile = await _userProfileService.GetProfile(userId);
                if (userProfile != null)
                {
                    return Ok(userProfile);
                }

                var organizationProfile = await _organizationProfileService.GetOrganizationProfile(userId);
                if (organizationProfile != null)
                {
                    return Ok(organizationProfile);
                }

                return NotFound($"Profile with user ID {userId} not found in either User or Organization profiles.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // PUT: api/profile/update-profile/{userId}
        [HttpPut("update-User-profile/{userId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateUserProfile(int userId, [FromBody] ProfileUpdateDTO profileUpdateDTO)
        {
            try
            {
                // Get the user profile first to check if it exists
                var userProfile = await _userProfileService.GetProfile(userId);
                if (userProfile == null)
                {
                    return NotFound($"Profile with user ID {userId} not found.");
                }

                // Now update the user profile
                var updatedUserProfile = await _userProfileService.UpdateProfile(userId, profileUpdateDTO);
                if (updatedUserProfile != null)
                {
                    return Ok(updatedUserProfile);
                }

                // If the update fails for some reason, return a bad request
                return BadRequest("Failed to update the user profile.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // PUT: api/profile/update-profile/{userId}
        [HttpPut("update-Organization-profile/{userId}")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<IActionResult> UpdateOrgProfile(int userId, [FromBody] OrganizationProfileDTO organizationProfileDTO)
        {
            try
            {
                // Get the organization profile first to check if it exists
                var organizationProfile = await _organizationProfileService.GetOrganizationProfile(userId);
                if (organizationProfile == null)
                {
                    return NotFound($"Profile with user ID {userId} not found in Organization.");
                }

                // Now update the organization profile
                var updatedOrganizationProfile = await _organizationProfileService.UpdateOrganizationProfile(userId, organizationProfileDTO);
                if (updatedOrganizationProfile != null)
                {
                    return Ok(updatedOrganizationProfile);
                }

                // If the update fails for some reason, return a bad request
                return BadRequest("Failed to update the organization profile.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating organization profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }
}

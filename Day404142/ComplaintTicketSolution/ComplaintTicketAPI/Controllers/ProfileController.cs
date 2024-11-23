using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
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
        [HttpGet("Get-profile")]
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


        [HttpPut("update-User-profile")]
       [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateUserProfile(int userId, [FromForm] ProfileUpdateDTO profileUpdateDTO)
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

        
        [HttpPut("update-Organization-profile")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<IActionResult> UpdateOrgProfile(int userId, [FromForm] OrganizationProfileDTO organizationProfileDTO)
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

        [HttpGet("Get-ALl-organizations-profile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrgProfiles()
        {
            try
            {
                var orgProfiles = await _userProfileService.GetALLOrgProfile();
                if (orgProfiles == null || orgProfiles.Count == 0)
                {
                    return NotFound("No organization profiles found.");
                }

                return Ok(orgProfiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint to get all user profiles
        [HttpGet("Get-ALl-users-profile")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            try
            {
                var userProfiles = await _userProfileService.GetALLUserProfile();
                if (userProfiles == null || userProfiles.Count == 0)
                {
                    return NotFound("No user profiles found.");
                }

                return Ok(userProfiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    


}
}

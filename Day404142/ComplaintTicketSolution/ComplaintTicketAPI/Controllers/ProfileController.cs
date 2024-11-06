using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
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

        // GET: api/profile/user/{userId}
        [HttpGet("GetUserProfile")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            try
            {
                var profile = await _userProfileService.GetProfile(userId);
                if (profile == null)
                {
                    return NotFound($"User profile with user ID {userId} not found.");
                }
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // PUT: api/profile/user/{userId}
        [HttpPut("UpdateUserProfile")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUserProfile(int userId, ProfileUpdateDTO updateDto)
        {
            try
            {
                var updatedProfile = await _userProfileService.UpdateProfile(userId, updateDto);
                if (updatedProfile == null)
                {
                    return NotFound($"User profile with user ID {userId} not found.");
                }
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET: api/profile/organization/{userId}
        [HttpGet("GetOrganizationProfile")]
        [Authorize(Roles = "Organization")]
        public async Task<IActionResult> GetOrganizationProfile(int userId)
        {
            try
            {
                var organization = await _organizationProfileService.GetOrganizationProfile(userId);
                if (organization == null)
                {
                    return NotFound($"Organization profile with user ID {userId} not found.");
                }
                return Ok(organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving organization profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // PUT: api/profile/organization/{userId}
        [HttpPut("UpdateOrganizationProfile")]
        [Authorize(Roles = "Organization")]


        public async Task<IActionResult> UpdateOrganizationProfile(int userId, OrganizationProfileDTO updateDto)
        {
            try
            {
                var updatedOrganization = await _organizationProfileService.UpdateOrganizationProfile(userId, updateDto);
                if (updatedOrganization == null)
                {
                    return NotFound($"Organization profile with user ID {userId} not found.");
                }
                return Ok(updatedOrganization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating organization profile.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}

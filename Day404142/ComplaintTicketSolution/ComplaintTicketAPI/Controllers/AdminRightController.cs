using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRightController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userpService;
        private readonly IComplaintGetService _complaintDetailService;
        private readonly IOrganizationService _organizationService;
        
        public AdminRightController(IUserService userService,
            IOrganizationService organizationService,
            IComplaintGetService complaintDetailService ,
            IUserProfileService userProfileService)
        {
            _userService = userService;
            _organizationService = organizationService;
            _complaintDetailService = complaintDetailService;
            _userpService = userProfileService;
        }

        [HttpGet("GetDashboardSummary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                // Fetch all complaints
                var complaints = await _complaintDetailService.GetComplaintsAsync(1,10);

                // Fetch all users and organizations
                var totalUsers = (await _userpService.GetALLUserProfile()).Count();
                
                var totalOrganizations = (await _organizationService.GetAllOrganizationsAsync()).Count();

                //var complaintsByStatus = complaints
                //    .GroupBy(c => c.ComplaintStatusDates)
                //    .ToDictionary(g => g.Key, g => g.Count());

                // Prepare response object
                var dashboardSummary = new
                {
                    //TotalComplaints = complaints.Count(),
                    TotalUsers = totalUsers,
                    TotalOrganizations = totalOrganizations,
                    //ComplaintsByStatus = complaintsByStatus
                };

                return Ok(dashboardSummary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the dashboard summary.");
            }
        }

        // Reactivate user (soft delete)
        [HttpPut("reactivate")]
        public async Task<IActionResult> ReactivateUser(string key)
        {
            try
            {
                var user = await _userService.ReactivateUserAsync(key);
                return Ok(new
                {
                    Success = true,
                    Message = "User reactivated successfully.",
                    Data = user
                });
            }
            catch (CouldNotDeleteException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while reactivating the user.",
                    Error = ex.Message
                });
            }
        }

        // Soft delete user
        [HttpDelete("Deactivating")]
        public async Task<IActionResult> SoftDeleteUser(string key)
        {
            try
            {
                var user = await _userService.SoftDeleteUserAsync(key);
                return Ok(new
                {
                    Success = true,
                    Message = "User deactivated successfully.",
                    Data = user
                });
            }
            catch (CouldNotDeleteException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while deactivating the user.",
                    Error = ex.Message
                });
            }
        }

    }
}


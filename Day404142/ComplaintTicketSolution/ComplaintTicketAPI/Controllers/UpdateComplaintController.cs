using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class UpdateComplaintController : ControllerBase
    {
        private readonly IUpdateComplaintService _updateComplaintService;

        public UpdateComplaintController(IUpdateComplaintService updateComplaintService)
        {
            _updateComplaintService = updateComplaintService;
        }

        [HttpGet("{orgId}/GetComplaints")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<ActionResult<Complaint>> GetComplaint(int orgId)
        {
            try
            {
                var complaint = await _updateComplaintService.GetComplaintByOrganizationIdAsync(orgId);
                return Ok(complaint);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateComplaintStatus")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<IActionResult> UpdateComplaintStatus([FromBody] UpdateComplaintRequestDTO updateRequest)
        {
            try
            {
                var isUpdated = await _updateComplaintService.UpdateComplaintStatusAsync(updateRequest);
                if (isUpdated==true)
                    return Ok("Updated Complaint. Thankyou"); // Successfully updated, return 204 No Content

                return BadRequest("Unable to update complaint status."); // If update fails
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

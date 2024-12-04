using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Repositories;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class UpdateComplaintController : ControllerBase
    {
        private readonly IUpdateComplaintService _updateComplaintService;
        private readonly IComplaintDetailService _complaint;
        public UpdateComplaintController(IUpdateComplaintService updateComplaintService, IComplaintDetailService complaint)
        {
            _updateComplaintService = updateComplaintService;
            _complaint = complaint;
        }



        [HttpPut("UpdateComplaintStatus")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<IActionResult> UpdateComplaintStatus([FromBody] UpdateComplaintRequestDTO updateRequest)
        {
            try
            {
                var complaint = await _complaint.GetComplaintDetailsAsync(updateRequest.ComplaintId);
                if (complaint == null)
                {
                    return NotFound(new { Message = "Complaint not found" });
                }

                if (updateRequest.Status == Status.Recieved && complaint.Status== "InProgress")
                {
                    return BadRequest(new UpdateComplaintResponseDTO
                    {
                        ComplaintId = updateRequest.ComplaintId,
                        Status = updateRequest.Status,
                        Message = "Already in progress"
                    });
                }

                // Check if the complaint status is "Resolved" and prevent any further updates
                if (complaint.Status == "Resolved")
                {
                    return BadRequest(new UpdateComplaintResponseDTO
                    {
                        ComplaintId = updateRequest.ComplaintId,
                        Status = updateRequest.Status,
                        Message = "Already resolved"
                    });

                }

                var updatedComplaint = await _updateComplaintService.UpdateComplaintStatusAsync(updateRequest);


                if (updatedComplaint != null)
                {
                    // Assuming the service returns the updated complaint details
                    return Ok(new UpdateComplaintResponseDTO
                    {
                        ComplaintId = updateRequest.ComplaintId,
                        Status = updateRequest.Status,
                        UpdatedAt = DateTime.UtcNow, // Example additional field
                        Message = "Complaint updated successfully"
                    });
                }

                return BadRequest(new UpdateComplaintResponseDTO
                {
                    ComplaintId = updateRequest.ComplaintId,
                    Status = updateRequest.Status,
                    Message = "Unable to update complaint status"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = $"Internal server error: {ex.Message}" });
            }
        }

    }
}

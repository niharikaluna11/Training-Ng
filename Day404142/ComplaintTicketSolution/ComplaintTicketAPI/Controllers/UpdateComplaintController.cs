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


       
        public static ComplaintDataDTO ToComplaintDataDTO(Complaint complaint)
        {
            // Validate that ComplaintStatusDates is not null or empty
            var latestStatusDate = complaint.ComplaintStatusDates?
                                         .OrderByDescending(c => c.StatusDate)
                                         .FirstOrDefault();

            return new ComplaintDataDTO
            {
                Id = complaint.Id,
                Description = complaint.Description,
                Status = latestStatusDate?.ComplaintStatus.Status ?? Status.Recieved,
                Priority = latestStatusDate?.ComplaintStatus.Priority ?? Priority.Low,
                CategoryName = complaint.Category?.Name,
                LastUpdated = latestStatusDate?.StatusDate ?? DateTime.Now
            };
        }


        [HttpGet("GetComplaints")]
        [Authorize(Roles = "Admin,Organization")]
        public async Task<ActionResult<List<ComplaintDataDTO>>> GetComplaints(int orgId)
        {
            try
            {
                var complaints = await _updateComplaintService.GetComplaintByOrganizationIdAsync(orgId);
                if (complaints == null || !complaints.Any())
                {
                    return NotFound();
                }

                var complaintDTOs = complaints.Select(ToComplaintDataDTO).ToList();
                return Ok(complaintDTOs);
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

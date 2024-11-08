using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Context;
using AutoMapper;

[Route("api/[controller]")]
[ApiController]
public class ComplaintController : ControllerBase
{
    private readonly IComplaintService _complaintService;
    private readonly ComplaintTicketContext _context;
    private readonly IMapper _mapper;

    // Constructor with dependency injection
    public ComplaintController(IComplaintService complaintService, ComplaintTicketContext context, IMapper mapper)
    {
        _complaintService = complaintService;
        _context = context;
        _mapper = mapper;
    }

    // POST: api/complaint
    [HttpPost("FileComplaint")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintRequestDTO complaintDto)
    {
        if (complaintDto == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid complaint data.");
        }

        try
        {
            // Call the service to create a complaint and set default status
            var createdComplaint = await _complaintService.CreateComplaint(complaintDto);

            // Return the created complaint
            return CreatedAtAction(nameof(GetComplaint), new { id = createdComplaint.Id }, createdComplaint);
        }
        catch (InvalidOperationException ex)
        {
            // If complaint already exists for this OrganizationId, handle it here
            return Conflict(ex.Message); // Return a 409 Conflict status code with the error message
        }
        catch (Exception ex)
        {
            // Handle other errors
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/complaint/{id}
    [HttpGet("GetComplaintBy/{userid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<Complaint>> GetComplaint(int id)
    {
        try
        {
            // Call the service to get a specific complaint by id
            var complaint = await _complaintService.GetComplaint(id);

            return Ok(complaint);
        }
        catch (KeyNotFoundException ex)
        {
            // Handle when the complaint is not found
            return NotFound(ex.Message); // Return a 404 Not Found status code with the error message
        }
        catch (Exception ex)
        {
            // Handle other errors
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("TrackComplaintStatus/{complaintId}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<ComplaintStatusDTO>> TrackComplaintStatus(int complaintId)
    {
        try
        {
            // Fetch the complaint including its status dates
            var complaint = await _context.Complaints
                                          .Include(c => c.ComplaintStatusDates)  // Include status dates
                                          .FirstOrDefaultAsync(c => c.Id == complaintId); // Ensure 'Id' matches your database column name

            if (complaint == null)
            {
                return NotFound("Complaint not found.");
            }

            // Ensure you are ordering by the correct column for the latest status
            var latestStatus = complaint.ComplaintStatusDates
                                        .OrderByDescending(cs => cs.StatusDate)  // Assuming StatusDate exists and is correct
                                        .FirstOrDefault();

            if (latestStatus == null)
            {
                return NotFound("No status found for this complaint.");
            }

            // Map to a DTO to return relevant status information
            var complaintStatusDTO = _mapper.Map<ComplaintStatusDTO>(latestStatus.ComplaintStatus);

            if (complaintStatusDTO == null)
            {
                return StatusCode(500, "Failed to map complaint status to DTO.");
            }

            // Add other relevant information like date of status change
            complaintStatusDTO.StatusDate = latestStatus.StatusDate;
            complaintStatusDTO.Priority = latestStatus.ComplaintStatus.Priority;

            return Ok(complaintStatusDTO);
        }
        catch (Exception ex)
        {
            // Handle other errors
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}

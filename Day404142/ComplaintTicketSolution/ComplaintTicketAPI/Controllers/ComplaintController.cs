using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Context;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

[Route("api/[controller]")]
[EnableCors("AllowAll")]
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

    [HttpGet("GetComplaintById")] // Updated route parameter to match CreateComplaint usage
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> GetComplaint(int id)
    {
        try
        {
            var complaint = await _complaintService.GetComplaint(id);

            if (complaint == null)
            {
                return NotFound("Complaint not found.");
            }

            return Ok(complaint);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("FileComplaint")]
   [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> CreateComplaint([FromForm] CreateComplaintRequestDTO complaintDto)
    {
        if (complaintDto == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid complaint data.");
        }

        try
        {
            var createdComplaint = await _complaintService.CreateComplaint(complaintDto);

           
            return Ok(createdComplaint);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpGet("TrackComplaintStatus")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult> TrackComplaintStatus(int complaintId)
    {
        try
        {
            // Fetch the complaint including its status dates
            var complaint = await _context.Complaints
                                          .Include(c => c.ComplaintStatusDates)
                                          .ThenInclude(cs => cs.ComplaintStatus)
                                          .FirstOrDefaultAsync(c => c.Id == complaintId);

            if (complaint == null)
            {
                return NotFound("Complaint not found.");
            }

            // Select only the relevant fields from each status in the complaint
            var result = complaint.ComplaintStatusDates
                                  .OrderByDescending(cs => cs.StatusDate)
                                  .Select(cs => new
                                  {
                                      Status = cs.ComplaintStatus.Status,
                                      CommentByUser = cs.ComplaintStatus.CommentByUser,
                                      StatusDate = cs.StatusDate
                                  })
                                  .ToList(); // Convert the results to a list

            // Return the list in the response
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("GenerateComplaintReport")]
    [Authorize(Roles = "Admin,Organization")]
    public async Task<ActionResult> GenerateComplainReport(int OrgId)
    {
        try
        {
            // Fetch the complaints and their status details, including the status dates
            var complaints = await _context.Complaints
                .Include(c => c.ComplaintStatusDates)
                .ThenInclude(cs => cs.ComplaintStatus)
                .Where(c => c.OrganizationId == OrgId)
                .ToListAsync();

            if (complaints == null || !complaints.Any())
            {
                return NotFound("Complaints not found for the specified organization.");
            }

            // Get the latest status for each complaint
            var latestStatusComplaints = complaints
                .Select(c => new
                {
                    ComplaintId = c.Id,
                    LatestStatus = c.ComplaintStatusDates
                        .OrderByDescending(cs => cs.StatusDate)  // Order by the latest status date
                        .FirstOrDefault()?.ComplaintStatus.Status  // Get the status of the most recent entry
                })
                .Where(x => x.LatestStatus != null)  // Exclude complaints without a status
                .ToList();

            // If there are no valid status updates, return an appropriate message
            if (!latestStatusComplaints.Any())
            {
                return NotFound("No valid status updates found for the complaints.");
            }

            // Count how many complaints have the latest status
            var statusCount = latestStatusComplaints
             .GroupBy(x => x.LatestStatus)  // Group by latest status
             .Select(g => new
             {
                 Status = g.Key.ToString(),  // Get the status name as a string (if it's an enum)
                 Count = g.Count()
             })
             .ToList();




            // Combine the total complaint count and the status count
            var result = new
            {
                TotalComplaints = complaints.Count,  // Total complaints
                LatestStatusCounts = statusCount  // Count of complaints by latest status
            };

            // Return the result in the response
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




}

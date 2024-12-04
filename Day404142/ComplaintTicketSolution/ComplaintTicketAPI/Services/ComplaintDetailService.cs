using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;  // Assuming you have models for User and Organization
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class ComplaintDetailService : IComplaintDetailService
    {
        private readonly ComplaintTicketContext _context;

        public ComplaintDetailService(ComplaintTicketContext context)
        {
            _context = context;
        }

    
        public async Task<string> GetComplaintCategoryAsync(int categoryId)
        {
            var category = await _context.ComplaintCategories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            return category?.Name ?? "Unknown";
        }

        public async Task<List<string>> GetComplaintFilesAsync(int complaintId)
        {
            var files = await _context.ComplaintFiles
                .Where(cf => cf.ComplaintId == complaintId)
                .Select(cf => cf.FilePath)
                .ToListAsync();
            return files;
        }

        public async Task<(string Status, string Priority, DateTime? LatestStatusDate)> GetLatestStatusAsync(int complaintId)
        {
           
            var latestStatusDate = await _context.ComplaintStatusDates
                .Where(cs => cs.ComplaintId == complaintId)
                  .OrderByDescending(cs => cs.ComplaintStatusId) 
                     .FirstOrDefaultAsync();
           

            if (latestStatusDate != null)
            {
                var status = await _context.ComplaintStatuses
                    .Where(s => s.Id == latestStatusDate.ComplaintStatusId)
                    .FirstOrDefaultAsync();


                if (status != null)
                {
                    return (status.Status.ToString(), status.Priority.ToString(), latestStatusDate.StatusDate);
                }
            }

            // Return "Unknown" if no status is found, and null for the status date
            return ("Unknown", "Unknown", null);
        }


        public async Task<(DateTime? StatusDate, string Status)> GetLatestStatusDateAsync(int complaintId)
        {
            
            var latestStatusDate = await _context.ComplaintStatusDates
                .Where(cs => cs.ComplaintId == complaintId)
                .OrderByDescending(cs => cs.StatusDate)
                .FirstOrDefaultAsync();

        
            if (latestStatusDate != null)
            {
                var status = await _context.ComplaintStatuses
                    .Where(s => s.Id == latestStatusDate.ComplaintStatusId)
                    .FirstOrDefaultAsync();

                if (status != null)
                {
                    return (latestStatusDate.StatusDate, status.Status.ToString());
                }
            }

        
            return (null, "Unknown");
        }

        public async Task<User> GetUserDetailsAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.userId == userId);

            return user; // Returns null if user not found
        }

        public async Task<Organization> GetOrganizationDetailsAsync(int orgId)
        {
            var organization = await _context.Organizations
                .FirstOrDefaultAsync(o => o.orgId == orgId);

            return organization; // Returns null if organization not found
        }

        public async Task<ComplaintDetailDto> GetComplaintDetailsAsync(int complaintId)
        {
            // Get Category Name
            var categoryName = await GetComplaintCategoryAsync(await _context.Complaints
                .Where(c => c.ComplaintId == complaintId)
                .Select(c => c.CategoryId)
                .FirstOrDefaultAsync());

            // Get Complaint Files
            var files = await GetComplaintFilesAsync(complaintId);

            // Get Latest Status and Priority
            var (status, priority,statusdate) = await GetLatestStatusAsync(complaintId);

            // Get Latest Status Date
            var statusDate = await GetLatestStatusDateAsync(complaintId);

            // Get User Details
            var userId = await _context.Complaints
                .Where(c => c.ComplaintId == complaintId)
                .Select(c => c.UserId)
                .FirstOrDefaultAsync();
            var userDetails = await GetUserDetailsAsync(userId);

            // Get Organization Details
            var orgId = await _context.Complaints
                .Where(c => c.ComplaintId == complaintId)
                .Select(c => c.OrganizationId)
                .FirstOrDefaultAsync();
            var orgDetails = await GetOrganizationDetailsAsync(orgId);

            // Combine all details into a DTO
            var complaintDetails = new ComplaintDetailDto
            {
                ComplaintId = complaintId,
                CategoryName = categoryName,
                Files = files,
                Status = status,
                Priority = priority,
                LatestStatusDate = statusDate.StatusDate,
                UserDetails = userDetails,   // Include user details
                OrganizationDetails = orgDetails  // Include organization details
            };

            return complaintDetails;
        }
    }
}

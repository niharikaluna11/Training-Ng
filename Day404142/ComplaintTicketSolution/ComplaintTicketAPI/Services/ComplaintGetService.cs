using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class ComplaintGetService : IComplaintGetService
    {
        private readonly ComplaintTicketContext _context;
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<int, ComplaintStatus> _complaintStatusRepository;
        private readonly IComplaintFileRepository _complaintFileRepository;
        private readonly IRepository<int, ComplaintStatusDate> _complaintStatusDateRepository;
        private readonly ILogger<ComplaintService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserProfileService _userProfileService;
        private readonly IOrganizationProfileService _organizationProfileService;
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "ComplaintFiles");

        public ComplaintGetService(
            ComplaintTicketContext context,
            IComplaintRepository complaintRepository,
            IRepository<int, ComplaintStatus> complaintStatusRepository,
            IComplaintFileRepository complaintFileRepository,
            IRepository<int, ComplaintStatusDate> complaintStatusDateRepository,
            IMapper mapper,
            ILogger<ComplaintService> logger,
            IEmailSender emailSender,
            IUserProfileService userProfileService,
            IOrganizationProfileService organizationProfileService)
        {
            _context = context;
            _complaintRepository = complaintRepository;
            _complaintStatusRepository = complaintStatusRepository;
            _complaintFileRepository = complaintFileRepository;
            _complaintStatusDateRepository = complaintStatusDateRepository;
            _mapper = mapper;
            _logger = logger;
            _emailSender = emailSender;
            _userProfileService = userProfileService;
            _organizationProfileService = organizationProfileService;
        }


        public async Task<int> GetComplaintCountByOrganizationIdAsync(int orgId)
        {
            var complaints = await _complaintRepository.GetAll();
            var filteredComplaints = complaints.Where(c => c.OrganizationId == orgId).ToList();
            return filteredComplaints.Count;
        }
        public async Task<IEnumerable<Complaint>> GetComplaintByOrganizationIdAsync(int orgId, int pagenum, int pagesize)
        {
            try
            {
                // Get all complaints from the repository
                var complaints = await _complaintRepository.GetAll();

                // Filter complaints by organization ID
                var filteredComplaints = complaints.Where(c => c.OrganizationId == orgId).ToList();

                // Validate page number and page size
                pagenum = Math.Max(pagenum, 1);
                pagesize = Math.Max(pagesize, 5); // Default page size if not specified

                // Calculate the total number of complaints and pages
                int total = filteredComplaints.Count();
                int pageTotal = (int)Math.Ceiling((double)total / pagesize);

                // Paginate the complaints
                var returncomplaints = filteredComplaints
                                        .Skip((pagenum - 1) * pagesize)
                                        .Take(pagesize)
                                        .ToList();

                // If no complaints are found for this organization
                if (!returncomplaints.Any())
                {
                    throw new KeyNotFoundException("No complaints found for this organization.");
                }

                // Return the paginated list of complaints
                return returncomplaints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching complaints for organization ID: {OrgId}", orgId);
                throw new Exception("An error occurred while retrieving complaints.", ex);
            }
        }



    }
}

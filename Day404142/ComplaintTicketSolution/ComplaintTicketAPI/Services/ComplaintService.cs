using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Interfaces.InterfaceRepository;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ComplaintTicketAPI.Services
{
    public class ComplaintService : IComplaintService
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

        public ComplaintService(
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

     
        private void SendMail(string title, string email, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        email },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uniqueFileName = $"complaint_{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(_uploadFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string folderPath = "complaints/";

            var gitHubService = new GitHubService();
            var result = await gitHubService.SaveFileToGitHub(filePath, uniqueFileName, folderPath);

            return result;  
        }


        public async Task<UserProfile> GetProfile(int userId)
        {
            // Retrieve the profile from the database by userId
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            return profile; // Return the found profile
        }

        public async Task<Complaint> CreateComplaint(CreateComplaintRequestDTO complaintDto)
        {
            try
            {
                // Step 1: Create and save the complaint
                var complaint = _mapper.Map<Complaint>(complaintDto);
                var createdComplaint = await _complaintRepository.Add(complaint);

                // Step 2: Create and save the initial complaint status
                var complaintStatus = _mapper.Map<ComplaintStatus>(complaintDto);
                complaintStatus.Status = Status.Recieved;
                complaintStatus.Priority = Priority.Medium;
                await _complaintStatusRepository.Add(complaintStatus);

                // Step 3: Create and save the complaint files
                var complaintFiles = await SaveComplaintFiles(complaintDto.AttachmentUrl, createdComplaint.ComplaintId);
                if (complaintFiles.Any())
                {
                    await _complaintFileRepository.AddFiles(complaintFiles);  // Save the complaint files
                }

                // Step 4: Save the complaint status date
                await SaveComplaintStatusDate(createdComplaint.ComplaintId, complaintStatus.Id);

                // Step 5: Get user and organization profile
                var userProfile = await GetProfile(complaint.UserId);
                var orgProfile = await _organizationProfileService.GetOrganizationProfile(complaint.OrganizationId);

                // Step 6: Send emails
                if (userProfile != null && orgProfile != null)
                {
                    await SendComplaintNotificationEmails(userProfile, orgProfile, complaint, complaintStatus);
                }

                await _context.SaveChangesAsync();
                return createdComplaint;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error occurred while creating the complaint.");
                throw new Exception("An error occurred while creating the complaint.", dbEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the complaint.");
                throw new Exception("An unexpected error occurred while creating the complaint.", ex);
            }
        }

        private async Task<List<ComplaintFile>> SaveComplaintFiles(IEnumerable<IFormFile> attachmentUrls, int complaintId)
        {
            if (attachmentUrls == null || !attachmentUrls.Any())
            {
                Console.WriteLine("No attachments provided.");
                return new List<ComplaintFile>();
            }

            var complaintFiles = new List<ComplaintFile>();

            foreach (var file in attachmentUrls)
            {
                try
                {
                    Console.WriteLine($"Uploading file: {file.FileName}");

                    // Generate a unique name for the file
                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

                    var filePath = await SaveFileAsync(file);

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        Console.WriteLine($"File uploaded successfully: {file.FileName}");
                        complaintFiles.Add(new ComplaintFile
                        {
                            ComplaintId = complaintId,
                            FilePath = filePath
                        });
                    }
                    else
                    {
                        Console.WriteLine($"Failed to upload file: {file.FileName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error uploading file {file.FileName}: {ex.Message}");
                }

                // Optional: Introduce a delay to avoid GitHub API rate limits
                await Task.Delay(200);
            }

            Console.WriteLine($"{complaintFiles.Count} files uploaded successfully out of {attachmentUrls.Count()}.");
            return complaintFiles;
        }


        // Helper function to save complaint status date
        private async Task SaveComplaintStatusDate(int complaintId, int complaintStatusId)
        {
            var complaintStatusDate = new ComplaintStatusDate
            {
                ComplaintId = complaintId,
                ComplaintStatusId = complaintStatusId,
                StatusDate = DateTime.Now
            };
            await _complaintStatusDateRepository.Add(complaintStatusDate);
        }

        // Helper function to send complaint notification emails
        private async Task SendComplaintNotificationEmails(UserProfile userProfile, Organization orgProfile, Complaint complaint, ComplaintStatus complaintStatus)
        {
            try
            {
                // Organization email body
                string orgBody = $@"
            <html>
                <head><style>/* CSS Styling */</style></head>
                <body>
                    <div class='header'><h1>ComplaintTicketApp</h1></div>
                    <p>Dear <strong>{orgProfile.Name}</strong>,</p>
                    <p>A complaint has been received for you.</p>
                    <div class='details'>
                        <p><strong>Complaint ID:</strong> {complaint.ComplaintId}</p>
                        <p><strong>User:</strong> {userProfile.FirstName}</p>
                        <p><strong>Priority:</strong> {complaintStatus.Priority.ToString()}</p>
                        <p><strong>Comment By User:</strong> {complaintStatus.CommentByUser}</p>
                        <p><strong>Status:</strong> {complaintStatus.Status}</p>
                    </div>
                    <p class='footer'>Thank you for your attention.</p>
                    <p class='footer'>Best regards,<br/><span class='signature'>ComplaintTicketApp Team</span></p>
                </body>
            </html>";

                // User email body
                string userBody = $@"
            <html>
                <head><style>/* CSS Styling */</style></head>
                <body>
                    <div class='header'><h1>ComplaintTicketApp</h1></div>
                    <p>Dear <strong>{userProfile.FirstName} {userProfile.LastName}</strong>,</p>
                    <p>We are pleased to inform you that you have successfully filed a complaint.</p>
                    <div class='details'>
                        <p><strong>Complaint ID:</strong> {complaint.ComplaintId}</p>
                        <p><strong>Priority:</strong> {complaintStatus.Priority.ToString()}</p>
                        <p><strong>Organization ID:</strong> {complaint.OrganizationId}</p>
                        <p><strong>Comment By You:</strong> {complaintStatus.CommentByUser}</p>
                        <p><strong>Status:</strong> {complaintStatus.Status}</p>
                    </div>
                    <p class='footer'>Should you have any questions or require assistance, please feel free to contact our support team.</p>
                    <p class='footer'>Best regards,<br/><span class='signature'>ComplaintTicketApp Support Team</span><br/> (Niharika Garg)</p>
                </body>
            </html>";

                // Send emails to both user and organization
                 SendMail("Complaint Successfully Filed", userProfile.Email, userBody);
                 SendMail("Complaint Received Notification", orgProfile.Email, orgBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending notification emails.");
                throw new Exception("Mail cannot be sent because of invalid mail", ex);
            }
        }



        public async Task<Complaint> GetComplaint(int id)
        {
            try
            {
                var complaint = await _context.Complaints
                                         .Include(c => c.ComplaintFiles)
                                              .FirstOrDefaultAsync(c => c.ComplaintId == id);

               

                if (complaint == null)
                {
                    throw new KeyNotFoundException("Complaint not found.");
                }

                return complaint;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving the complaint with ID: {ComplaintId}", id);
                throw new Exception("An error occurred while retrieving the complaint.", ex);
            }
        }

        public async Task<List<Complaint>> GetAllComplaint()
        {
            try
            {
                var complaints = await _context.Complaints.ToListAsync();

                if (complaints == null || complaints.Count == 0)
                {
                    throw new KeyNotFoundException("No Complaints Found.");
                }

                return complaints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all complaints.");
                throw new Exception("An error occurred while retrieving all complaints.", ex);
            }
        }

        public async Task<ComplaintStatusDTO> TrackComplaintStatus(int complaintId)
        {
            try
            {
                var complaint = await _context.Complaints
                                              .Include(c => c.ComplaintStatusDates)  // Include status dates
                                              .FirstOrDefaultAsync(c => c.ComplaintId == complaintId);

                if (complaint == null)
                {
                    throw new KeyNotFoundException("Complaint not found.");
                }

                // Get the most recent complaint status
                var latestStatus = complaint.ComplaintStatusDates.OrderByDescending(cs => cs.StatusDate).FirstOrDefault();

                if (latestStatus == null)
                {
                    throw new Exception("No status found for this complaint.");
                }

                // Map to a DTO to return relevant status information
                var complaintStatusDTO = _mapper.Map<ComplaintStatusDTO>(latestStatus.ComplaintStatus);

                // Add other relevant information like date of status change
                complaintStatusDTO.StatusDate = latestStatus.StatusDate;
                complaintStatusDTO.Priority = latestStatus.ComplaintStatus.Priority;

                return complaintStatusDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while tracking status for complaint with ID: {ComplaintId}", complaintId);
                throw new Exception("An error occurred while tracking the complaint status.", ex);
            }
        }

        public async Task<List<Complaint>> GetComplaintsByCategoryId(int categoryId)
        {
            try
            {
                // Fetch complaints filtered by CategoryId
                var complaints = await _context.Complaints
                                               .Where(c => c.CategoryId == categoryId)
                                               .ToListAsync();

                if (complaints == null || complaints.Count == 0)
                {
                    throw new KeyNotFoundException("No Complaints found for the specified category.");
                }

                return complaints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving complaints by category.");
                throw new Exception("An error occurred while retrieving complaints by category.", ex);
            }
        }

        public async Task<string> GetComplaintCategory(int categoryid)
        {
            try
            {
                var category = await _context.ComplaintCategories
                    .FirstOrDefaultAsync(c => c.Id == categoryid);

                if (category == null)
                {
                    throw new KeyNotFoundException("category not found.");
                }

                return category.Name ?? "Unknown";  // Return "Unknown" if category is not set
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while retrieving the category for complaint ID: {ComplaintId}", complaintId);
                throw new Exception("An error occurred while retrieving the complaint category.", ex);
            }
        }


    }
}

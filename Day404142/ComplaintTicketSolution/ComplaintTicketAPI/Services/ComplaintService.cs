using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using ComplaintTicketAPI.Interfaces;
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
        private readonly IRepository<int, ComplaintFile> _complaintFileRepository;
        private readonly IRepository<int, ComplaintStatusDate> _complaintStatusDateRepository;
        private readonly ILogger<ComplaintService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserProfileService _userProfileService;
        private readonly IOrganizationProfileService _organizationProfileService;

        public ComplaintService(
            ComplaintTicketContext context,
            IComplaintRepository complaintRepository,
            IRepository<int, ComplaintStatus> complaintStatusRepository,
            IRepository<int, ComplaintFile> complaintFileRepository,
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

                // Step 3: Create and save the complaint file
                var complaintFile = _mapper.Map<ComplaintFile>(complaintDto);
                complaintFile.ComplaintId = createdComplaint.Id;
                await _complaintFileRepository.Add(complaintFile);


                var complaintStatusDate = new ComplaintStatusDate
                {
                    ComplaintId = createdComplaint.Id,
                    ComplaintStatusId = complaintStatus.Id,
                    StatusDate = DateTime.Now
                };
                await _complaintStatusDateRepository.Add(complaintStatusDate);

                var userProfile = await GetProfile(complaint.UserId);
                var orgprofile = await _organizationProfileService.GetOrganizationProfile(complaint.OrganizationId);

                if (userProfile != null)

                { 

                    try
                    {

                        string body = $"Dear {userProfile.FirstName}"+" "+"{userProfile.LastName},\n\n" +
                             "We are pleased to inform you that you have successfully filed a complaint.\n\n" +
                             "Below are the details of your complaint:\n\n" +
                             $"• Complaint ID: {complaint.Id}\n" +
                             $"• Priority: {complaintStatus.Priority.ToString()}\n" +
                             $"• Organization ID: {complaint.OrganizationId}\n" +
                             $"• Comment By you: {complaintStatus.CommentByUser}\n" +
                             $"• Status: {complaintStatus.Status}\n\n" +
                             "Should you have any questions or require assistance, please feel free to contact our support team.\n\n" +
                             "Best regards,\n" +
                             "ComplaintTicketApp\n" +
                             "Support Team\n" +
                             "(Niharika Garg)";

                        string orgBody = $"Dear {orgprofile.Name},\n\n" +
                                         "A complaint has been recieved for you.\n\n" +
                                         "Complaint Details:\n" +
                                         $"• Complaint ID: {complaint.Id}\n" +
                                         $"• User: {userProfile.FirstName}\n" +  // Include username
                                         $"• Priority: {complaintStatus.Priority.ToString()}\n" + // Add more details if needed
                                         $"• Comment By User: {complaintStatus.CommentByUser}\n" +
                                         $"• Status: {complaintStatus.Status}\n\n" +
                                         "Thank you for your attention.\n\n" +
                                         "Best regards,\n" +
                                         "ComplaintTicketApp Team";

                        string uemail = userProfile.Email.ToString();
                        string oemail = orgprofile.Email;
                        SendMail("Complaint Successfully Filed", userProfile.Email.ToString(), body);
                        SendMail("Complaint Received Notification", oemail, orgBody);


                    }

                    catch { throw new Exception("Mail Cannot be send Becuase of Invalid Mail"); }
            }

                await _context.SaveChangesAsync();
               // return new createdComplaint { Username = user.Username }
                return createdComplaint;
            }
            catch (DbUpdateException dbEx)
            {
                // Log database update errors
                _logger.LogError(dbEx, "Error occurred while creating the complaint.");
                throw new Exception("An error occurred while creating the complaint.", dbEx);
            }
            catch (Exception ex)
            {
                // Log general errors
                _logger.LogError(ex, "An unexpected error occurred while creating the complaint.");
                throw new Exception("An unexpected error occurred while creating the complaint.", ex);
            }
        }


        public async Task<Complaint> GetComplaint(int id)
        {
            try
            {
                var complaint = await _context.Complaints
                                              // .Include(c => c.ComplaintStatusDates)  // Include related entities if needed
                                              .FirstOrDefaultAsync(c => c.Id == id);

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
                                              .FirstOrDefaultAsync(c => c.Id == complaintId);

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
    }
}

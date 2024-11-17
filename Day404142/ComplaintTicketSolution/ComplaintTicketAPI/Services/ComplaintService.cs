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

                        string orgBody = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f0f8ff;
                                            color: #333;
                                        }}
                                        .header {{
                                            background-color: #0073e6;
                                            color: white;
                                            padding: 10px;
                                            text-align: center;
                                            font-size: 24px;
                                        }}
                                        .greeting {{
                                            font-size: 18px;
                                            color: #333;
                                        }}
                                        .content {{
                                            margin-top: 20px;
                                            color: #333;
                                        }}
                                        .details {{
                                            margin-top: 10px;
                                            font-weight: bold;
                                        }}
                                        .footer {{
                                            margin-top: 20px;
                                            font-size: 14px;
                                            color: #555;
                                        }}
                                        .signature {{
                                            color: #0073e6;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='header'>
                                        <h1>ComplaintTicketApp</h1>
                                    </div>

                                    <p class='greeting'>Dear <strong>{orgprofile.Name}</strong>,</p>

                                    <p class='content'>A complaint has been received for you.</p>

                                    <p class='content'>Complaint Details:</p>
                                    <div class='details'>
                                        <p><strong>Complaint ID:</strong> {complaint.Id}</p>
                                        <p><strong>User:</strong> {userProfile.FirstName}</p>
                                        <p><strong>Priority:</strong> {complaintStatus.Priority.ToString()}</p>
                                        <p><strong>Comment By User:</strong> {complaintStatus.CommentByUser}</p>
                                        <p><strong>Status:</strong> {complaintStatus.Status}</p>
                                    </div>

                                    <p class='content footer'>Thank you for your attention.</p>

                                    <p class='footer'>Best regards,<br/><span class='signature'>ComplaintTicketApp Team</span></p>
                                </body>
                                </html>";
                     
                     string userBody = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f0f8ff;
                                            color: #333;
                                        }}
                                        .header {{
                                            background-color: #0073e6;
                                            color: white;
                                            padding: 10px;
                                            text-align: center;
                                            font-size: 24px;
                                        }}
                                        .greeting {{
                                            font-size: 18px;
                                            color: #333;
                                        }}
                                        .content {{
                                            margin-top: 20px;
                                            color: #333;
                                        }}
                                        .details {{
                                            margin-top: 10px;
                                            font-weight: bold;
                                        }}
                                        .footer {{
                                            margin-top: 20px;
                                            font-size: 14px;
                                            color: #555;
                                        }}
                                        .signature {{
                                            color: #0073e6;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='header'>
                                        <h1>ComplaintTicketApp</h1>
                                    </div>

                                    <p class='greeting'>Dear <strong>{userProfile.FirstName} {userProfile.LastName}</strong>,</p>

                                    <p class='content'>We are pleased to inform you that you have successfully filed a complaint.</p>

                                    <p class='content'>Below are the details of your complaint:</p>
                                    <div class='details'>
                                        <p><strong>Complaint ID:</strong> {complaint.Id}</p>
                                        <p><strong>Priority:</strong> {complaintStatus.Priority.ToString()}</p>
                                        <p><strong>Organization ID:</strong> {complaint.OrganizationId}</p>
                                        <p><strong>Comment By You:</strong> {complaintStatus.CommentByUser}</p>
                                        <p><strong>Status:</strong> {complaintStatus.Status}</p>
                                    </div>

                                    <p class='content footer'>Should you have any questions or require assistance, please feel free to contact our support team.</p>

                                    <p class='footer'>Best regards,<br/><span class='signature'>ComplaintTicketApp Support Team</span><br/> (Niharika Garg)</p>
                                </body>
                                </html>";


                        string uemail = userProfile.Email.ToString();
                        string oemail = orgprofile.Email;
                        SendMail("Complaint Successfully Filed", userProfile.Email.ToString(), userBody);
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

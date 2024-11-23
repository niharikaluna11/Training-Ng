using System.Threading.Tasks;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ComplaintTicketAPI.EmailInterface;
using ComplaintTicketAPI.EmailModel;
using Microsoft.EntityFrameworkCore;
using ComplaintTicketAPI.Interfaces.InteraceServices;

namespace ComplaintTicketAPI.Services
{
    public class UpdateComplaintService : IUpdateComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateComplaintService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserProfileService _userProfileService;
        private readonly IOrganizationProfileService _organizationProfileService;

        public UpdateComplaintService(IComplaintRepository complaintRepository,
            IMapper mapper,
            ILogger<UpdateComplaintService> logger,
            IEmailSender emailSender,
            IUserProfileService userProfileService,
            IOrganizationProfileService organizationProfileService)
        {
            _complaintRepository = complaintRepository;
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

       

        public async Task<IEnumerable<Complaint>> GetComplaintByOrganizationIdAsync(int orgId)
        {
            try
            {
                // Get all complaints
                var complaints = await _complaintRepository.GetAll();

                // Filter the complaints to only include those that belong to the given orgId
                var filteredComplaints = complaints.Where(c => c.OrganizationId == orgId).ToList();

                if (!filteredComplaints.Any())
                {
                    // If no complaints match the given orgId, throw an exception
                    throw new KeyNotFoundException("No complaints found for this organization.");
                }

                return filteredComplaints;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching complaints for organization ID: {OrgId}", orgId);
                throw new Exception("An error occurred while retrieving complaints.", ex);
            }
        }


        public async Task<bool> UpdateComplaintStatusAsync(UpdateComplaintRequestDTO updateRequest)
        {
            try
            {
                // Fetch the existing complaint
                var complaint = await _complaintRepository.Get(updateRequest.ComplaintId);
                if (complaint == null || complaint.OrganizationId != updateRequest.OrganizationId)
                {
                    _logger.LogError("Complaint not found for organization.");
                    throw new KeyNotFoundException("Complaint not found for this organization.");
                }

                // Initialize ComplaintStatusDates if it's null
                if (complaint.ComplaintStatusDates == null)
                {
                    complaint.ComplaintStatusDates = new List<ComplaintStatusDate>();
                }

                // Map the updated fields from DTO and log the mapping
                var complaintStatus = _mapper.Map<ComplaintStatus>(updateRequest);
                _logger.LogInformation("Mapped complaint status: {Status}", complaintStatus.Status);

                // Add the new status update
                complaint.ComplaintStatusDates.Add(new ComplaintStatusDate
                {
                    ComplaintId = complaint.Id,
                    ComplaintStatus = complaintStatus,
                    StatusDate = updateRequest.StatusDate
                });

                // Attempt to update the complaint
                var updatedComplaint = await _complaintRepository.Update(complaint, complaint.Id);
                if (updatedComplaint == null)
                {
                    _logger.LogError("Complaint update failed, repository returned null.");
                    return false;
                }

                // Fetch user and organization profiles for email notification
                var userProfile = await _userProfileService.GetProfile(complaint.UserId);
                var orgProfile = await _organizationProfileService.GetOrganizationProfile(complaint.OrganizationId);

                if (userProfile != null && orgProfile != null)
                {
                    try
                    {
                        // Email content for user
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
                                <h1>Welcome to TicketSolve!</h1>
                            </div>

                            <p class='greeting'>Dear <strong>{userProfile.FirstName} {userProfile.LastName}</strong>,</p>
    
                            <p class='content'>The status of your complaint has been updated.</p>
    
                            <p class='content'>Complaint Details:</p>
                            <div class='details'>
                                <p><strong>Complaint ID:</strong> {complaint.Id}</p>
                                <p><strong>New Status:</strong> {complaintStatus.Status}</p>
                                <p><strong>Comment By Organization:</strong> {complaintStatus.CommentByUser}</p>
                                <p><strong>Status Updated On:</strong> {updateRequest.StatusDate}</p>
                            </div>

                            <p class='content footer'>Thank you for your patience.</p>
    
                            <p class='footer'>Best regards,<br/><span class='signature'>TicketSolve Team</span></p>
                        </body>
                        </html>";


                        // Send email to user
                        SendMail("Complaint Status Updated", userProfile.Email.ToString(), userBody);
                        _logger.LogInformation("Email sent to user {UserEmail}", userProfile.Email);
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx, "Error while sending email to user {UserEmail}", userProfile.Email);
                    }

                    try
                    {
                        // Email content for organization
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
                                    <h1>Welcome to TicketSolve!</h1>
                                </div>

                                <p class='greeting'>Dear <strong>{orgProfile.Name}</strong>,</p>
    
                                <p class='content'>A complaint status has been updated.</p>
    
                                <p class='content'>Complaint Details:</p>
                                <div class='details'>
                                    <p><strong>Complaint ID:</strong> {complaint.Id}</p>
                                    <p><strong>New Status:</strong> {complaintStatus.Status}</p>
                                    <p><strong>Comment:</strong> {complaintStatus.CommentByUser}</p>
                                    <p><strong>Status Date:</strong> {updateRequest.StatusDate}</p>
                                </div>

                                <p class='content footer'>Thank you for your attention.</p>
    
                                <p class='footer'>Best regards,<br/><span class='signature'>TicketSolve Team</span></p>
                            </body>
                            </html>";


                        // Send email to organization
                        SendMail("Complaint Status Update Notification", orgProfile.Email.ToString(), orgBody);
                        _logger.LogInformation("Email sent to organization {OrgEmail}", orgProfile.Email);
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx, "Error while sending email to organization {OrgEmail}", orgProfile.Email);
                    }
                }

                return true;
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogError(knfEx, "Complaint with ID {ComplaintId} not found for organization ID {OrgId}", updateRequest.ComplaintId, updateRequest.OrganizationId);
                throw new Exception("Complaint not found for the provided organization.", knfEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating complaint with ID {ComplaintId} for organization ID {OrgId}", updateRequest.ComplaintId, updateRequest.OrganizationId);
                throw new Exception("An error occurred while updating the complaint.", ex);
            }
        }



    }
}

using System.Threading.Tasks;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ComplaintTicketAPI.Services
{
    public class UpdateComplaintService : IUpdateComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateComplaintService> _logger;

        public UpdateComplaintService(IComplaintRepository complaintRepository, IMapper mapper, ILogger<UpdateComplaintService> logger)
        {
            _complaintRepository = complaintRepository;
            _mapper = mapper;
            _logger = logger;
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
                    throw new KeyNotFoundException("Complaint not found for this organization.");
                }

                // Initialize ComplaintStatusDates if it's null
                if (complaint.ComplaintStatusDates == null)
                {
                    complaint.ComplaintStatusDates = new List<ComplaintStatusDate>(); // Initialize the collection
                }

                // Map the updated fields from DTO
                var complaintStatus = _mapper.Map<ComplaintStatus>(updateRequest);

                // Add the new status update to the ComplaintStatusDates
                complaint.ComplaintStatusDates.Add(new ComplaintStatusDate
                {
                    ComplaintId = complaint.Id,
                    ComplaintStatus = complaintStatus,
                    StatusDate = updateRequest.StatusDate
                });

                // Update the complaint
                var updatedComplaint = await _complaintRepository.Update(complaint, complaint.Id);

                // Return true if update was successful
                return updatedComplaint != null;
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

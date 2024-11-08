using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ComplaintTicketAPI.Services
{
    public class ComplaintService : IComplaintService
    {

        private readonly ComplaintTicketContext _context;
        private readonly IRepository<int, Complaint> _complaintRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<int, ComplaintStatus> _complaintStatusRepository;
        private readonly IRepository<int, ComplaintFile> _complaintFileRepository;
        private readonly IRepository<int, ComplaintStatusDate> _complaintStatusDateRepository;

        public ComplaintService(
            ComplaintTicketContext context,
            IRepository<int, Complaint> complaintRepository,
            IRepository<int, ComplaintStatus> complaintStatusRepository,
            IRepository<int, ComplaintFile> complaintFileRepository,
            IRepository<int, ComplaintStatusDate> complaintStatusDateRepository,
            IMapper mapper)
        {
            _context = context;
            _complaintRepository = complaintRepository;
            _complaintStatusRepository = complaintStatusRepository;
            _complaintFileRepository = complaintFileRepository;
            _complaintStatusDateRepository = complaintStatusDateRepository;
            _mapper = mapper;
        }

        public async Task<Complaint> CreateComplaint(CreateComplaintRequestDTO complaintDto)
        {
            

            // Map CreateComplaintRequestDTO to Complaint entity
            var complaint = _mapper.Map<Complaint>(complaintDto);

            // Step 1: Add the complaint
            var createdComplaint = await _complaintRepository.Add(complaint);

            // Optional: Map ComplaintStatus (if needed)
            var complaintStatus = _mapper.Map<ComplaintStatus>(complaintDto);
            complaintStatus.Status = Status.Recieved; // Set default status to "Recieved"
            complaintStatus.Priority = Priority.Medium; // Set default priority if not provided
            await _complaintStatusRepository.Add(complaintStatus); // Save the ComplaintStatus

            // Optional: Map ComplaintFile (if needed)
            var complaintFile = _mapper.Map<ComplaintFile>(complaintDto);
            complaintFile.ComplaintId = createdComplaint.Id; // Ensure it links to the created complaint
            await _complaintFileRepository.Add(complaintFile); // Save the ComplaintFile

            // Optional: Map ComplaintStatusDate (if needed)
            var complaintStatusDate = new ComplaintStatusDate
            {
                ComplaintId = createdComplaint.Id,
                ComplaintStatusId = complaintStatus.Id, // Link to the complaint status
                StatusDate = DateTime.Now // Set the current date as the status date
            };
            await _complaintStatusDateRepository.Add(complaintStatusDate); // Save the ComplaintStatusDate

            // Save all changes to the database
            await _context.SaveChangesAsync();

            return createdComplaint;
        }


        public async Task<Complaint> GetComplaint(int id)
        {
            var complaint = await _context.Complaints
                                          .Include(c => c.ComplaintStatusDates)  // Include related entities if needed
                                          .FirstOrDefaultAsync(c => c.Id == id);

            if (complaint == null)
            {
                throw new KeyNotFoundException("Complaint not found.");
            }

            return complaint;
        }

        public async Task<List<Complaint>> GetAllComplaint()
        {
            var complaints = await _context.Complaints.ToListAsync();

            if (complaints == null || complaints.Count == 0)
            {
                throw new KeyNotFoundException("No Complaints Found.");
            }

            return complaints;
        }

        public async Task<ComplaintStatusDTO> TrackComplaintStatus(int complaintId)
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

    }
}

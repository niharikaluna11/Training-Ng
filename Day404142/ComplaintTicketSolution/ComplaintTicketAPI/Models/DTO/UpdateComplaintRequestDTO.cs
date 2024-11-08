namespace ComplaintTicketAPI.Models.DTO
{
    public class UpdateComplaintRequestDTO
    {
        public int ComplaintId { get; set; } // ID of the complaint being updated
        public int OrganizationId { get; set; } // ID of the organization

        public Status Status { get; set; } // New status for the complaint (Received, InProgress, Resolved)
        
        public string CommentByUser { get; set; } = string.Empty; // Comment provided by the user
        public DateTime StatusDate { get; set; } // Date of the status update
    }
}

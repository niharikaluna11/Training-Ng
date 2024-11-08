namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintStatusDTO
    {
        public int ComplaintId { get; set; } // Foreign key to Complaint
        public Status Status { get; set; } // Enum for status (Received, InProgress, Resolved)
        public Priority Priority { get; set; } // Enum for priority (Low, Medium, High, Urgent)

        public string CommentByUser { get; set; } = string.Empty;
        public DateTime StatusDate { get; set; } // Date of status update
    }
}

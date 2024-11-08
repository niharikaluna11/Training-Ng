namespace ComplaintTicketAPI.Models
{
    public class ComplaintStatus
    {
        
        public int Id { get; set; }
        public Status Status { get; set; } // Enum for status (Received, InProgress, Resolved)
        public Priority Priority { get; set; } // Enum for priority (Low, Medium, High, Urgent)

        public ICollection<ComplaintStatusDate> ComplaintStatusDates
        {
            get; set;

        }
        public string CommentByUser { get; set; } = string.Empty; 
    }
}


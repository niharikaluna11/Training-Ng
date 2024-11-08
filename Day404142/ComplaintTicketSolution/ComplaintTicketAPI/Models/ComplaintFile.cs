namespace ComplaintTicketAPI.Models
{
    public class ComplaintFile
    {
        public int Id { get; set; }
        public int ComplaintId { get; set; } // Foreign key to Complaint
        public Complaint Complaint { get; set; } // Navigation property to Complaint

        public string FilePath { get; set; } // Path to the file attachment
    }
}

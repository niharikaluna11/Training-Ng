using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models
{
    public class ComplaintStatusDate
    {
        [Key]
        public int Id { get; set; } // Unique ID for the status update

        public int ComplaintId { get; set; } // Foreign key to Complaint
        public Complaint Complaint { get; set; } // Navigation property to Complaint

        public int ComplaintStatusId { get; set; } // Foreign key to ComplaintStatus
        public ComplaintStatus ComplaintStatus { get; set; } // Navigation property to ComplaintStatus

        public DateTime StatusDate { get; set; } // Date and time when the status was updated
    }
}

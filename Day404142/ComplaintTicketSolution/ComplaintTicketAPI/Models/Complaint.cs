namespace ComplaintTicketAPI.Models
{
    public enum Status
    {
        Recieved =0,
        InProgress =1,
        Resolved =2
    }

    public enum Priority
    {
        Low =0,
        Medium =1,
        High =2,
        Urgent =3
    }


   
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property to User

        public int OrganizationId { get; set; } // Foreign key to Organization
        public string Description { get; set; }
     
        public int CategoryId { get; set; } // Foreign key to ComplaintCategory
        public ComplaintCategory Category { get; set; } // Navigation property to ComplaintCategory

        public ICollection<ComplaintFile> ComplaintFiles { get; set; } // 1:M relation to ComplaintFile

        public ICollection<ComplaintStatusDate> ComplaintStatusDates { get; set; } // M:M relation to ComplaintStatus through ComplaintStatusDate


       

    }
}

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


    public enum Category
    {
       Technical =1,
       CustomerService =2,
       Facilities =3,
       Others =0
    }
    public class Complaint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //foreign key 
        public User User { get; set; } // Navigation property to User

        // Foreign key to User
        public int OrganizationId { get; set; } 
        // Foreign key to Organization
        public Category Category { get; set; }
        //string or enum type
        public string Description { get; set; }
        public string AttachmentUrl { get; set; } 
        // URL for uploaded images/docs
        public Status Status { get; set; } 
        // Enum or string: Received, In Progress, Resolved
        public Priority Priority { get; set; } 
        // Enum or string: Low, Medium, High, Urgent

        public string CommentByUser { get; set; } = string.Empty;
        //String type Comment stored by user for organization

        public string CommentByOrg { get; set; } = string.Empty;
        //String type Comment stored by organization for user  

        public DateTime CreatedAt { get; set; }
    }
}

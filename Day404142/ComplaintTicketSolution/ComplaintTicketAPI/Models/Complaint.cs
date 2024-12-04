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
        public int UserId { get; set; } 
        public User User { get; set; } 

        public int OrganizationId { get; set; }
        public string Description { get; set; }
     
        public int CategoryId { get; set; } 
        public ComplaintCategory Category { get; set; } 

        public ICollection<ComplaintFile> ComplaintFiles { get; set; } 

        public ICollection<ComplaintStatusDate> ComplaintStatusDates { get; set; } 


       

    }
}

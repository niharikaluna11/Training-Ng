namespace ComplaintTicketAPI.Models
{
    public enum Type
    {
        Company = 1,
        Government= 2,
        Agent=3
    }
    public class Organization
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        //fk
        public string Name { get; set; }
        public String? ProfilePicture { get; set; }
        public Type Types { get; set; } 
        // Enum or string: Company, Government, Agent
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigation property for related reports

       // public PersonStatus PStatus { get; set; } = PersonStatus.Activated;

    }
}

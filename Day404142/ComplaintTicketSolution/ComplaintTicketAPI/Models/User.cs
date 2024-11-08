namespace ComplaintTicketAPI.Models
{
    public enum Role
    {
        Admin =0,
        User =1,
        Organization=2
    }
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }

        //stored hash password
        public UserProfile? Profile { get; set; }
        public Role Roles { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
            //= new List<Complaint>();
        public Organization? Organization { get;  set; }
       



    }

  
}

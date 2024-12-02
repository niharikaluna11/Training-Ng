using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models
{
    public enum Role
    {
        Admin =0,
        User =1,
        Organization= 2
    }

    public enum PersonStatus { 
        Activated=1, Deactivated=2
    }

    public class User
    {
        [Key] 
        public int userId { get; set; }
        
        public string Username { get; set; }
        public string Email { get; set; }

        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }

        //stored hash password
        public UserProfile? Profile { get; set; }
        public Role Roles { get; set; }
        public ICollection<Complaint> Complaints { get; set; }
            //= new List<Complaint>();
        public Organization? Organization { get;  set; }
        public bool IsDeleted { get; set; }

        public PersonStatus PStatus { get; set; } = PersonStatus.Activated;
        public string? ResetToken { get; set; }
        public DateTime? TokenExpiry { get; set; }

    }

  
}

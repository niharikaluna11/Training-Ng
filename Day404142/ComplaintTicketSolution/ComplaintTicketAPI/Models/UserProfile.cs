﻿namespace ComplaintTicketAPI.Models
{
    public class UserProfile
    {
        //User Profile

        public int Id { get; set; }
        public int UserId { get; set; } 
        // Foreign key to User
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public String? ProfilePicture { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Preferences { get; set; }

       // public PersonStatus PStatus { get; set; } = PersonStatus.Activated;
        // Any additional user preferences

    }
}

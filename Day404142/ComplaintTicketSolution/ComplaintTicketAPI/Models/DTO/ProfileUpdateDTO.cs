﻿namespace ComplaintTicketAPI.Models.DTO
{
    public class ProfileUpdateDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Preferences { get; set; }
       

    }
}


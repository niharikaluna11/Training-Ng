﻿namespace EFFirstAPI.Models.DTO
{
    public class CustomerDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public string Username { get; set; }



    }
}

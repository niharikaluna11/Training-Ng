﻿using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ComplaintTicketAPI.Models.DTO
{
    public class RegisterUserDto
    {

        [Required(ErrorMessage = "First Name is required")]
        public string Name { get;  set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username should be at least 5 characters long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password should be at least 5 characters long")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Role Role { get; set; }

        public Type Types { get; set; }

    }
}
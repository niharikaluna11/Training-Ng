﻿namespace TrialApplication.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
        public IEnumerable<Booking> Booking { get; set; }
        public IEnumerable<Attendance> Attendances { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public static class ClinicData
    {
        public static List<Patient> Patients { get; set; } = new List<Patient>();
        public static List<Doctor> Doctors { get; set; } = new List<Doctor>();

        //public static List<Patient> Patients { get; set; } = new List<Patient>
        //{
        //    new Patient { Id = 1, Name = "Alice Brown", Email = "alice.brown@example.com", PhoneNumber = "555-1234", UserName = "ng", Password = "123" },
        //    new Patient { Id = 2, Name = "Bob White", Email = "bob.white@example.com", PhoneNumber = "555-5678", UserName = "gg", Password = "123" }
        //};

        //public static List<Doctor> Doctors { get; set; } = new List<Doctor>
        //{
        //    new Doctor { Id = 1, Name = "Dr. Smith", Email = "smith@example.com", PhoneNumber = "123-456-7890", Specialization = "Cardiology", UserName = "nng", Password = "123" },
        //    new Doctor { Id = 2, Name = "Dr. Johnson", Email = "johnson@example.com", PhoneNumber = "987-654-3210", Specialization = "Neurology", UserName = "ggg", Password = "123" }
        //};

        public static List<Appointment> Appointments { get; set; } = new List<Appointment>();

      
        
    }
}

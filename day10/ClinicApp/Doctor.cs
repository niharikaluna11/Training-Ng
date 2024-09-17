﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public class Doctor : Person, IDoctor
    {
        public required string Specialization { get; set; }
        public List<Appointment> ViewAppointments()
        {
            if (ClinicData.Appointments.Count == 0)
            {
                Console.WriteLine("No Appointments available at the moment.");
                return new List<Appointment>();
            }

            return ClinicData.Appointments;
        }

        //public List<Patient> ViewMyPatients()
        //{
        //    if (MyPatients.Count == 0)
        //    {
        //        Console.WriteLine("You have no patients.");
        //    }
        //    return MyPatients;
        //}

        public List<Patient> ViewPatients()
        {
            if (ClinicData.Patients.Count == 0)
            {
                Console.WriteLine("No patients available at the moment.");
                return new List<Patient>();
            }

            // Returning all  patients from ClinicData
            return ClinicData.Patients;
        }

        
    }
}

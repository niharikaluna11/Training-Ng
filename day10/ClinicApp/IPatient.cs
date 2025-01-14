﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    //this is a interface Ipatient which is a person 
    public interface IPatient : IPerson
    {
        //doctor list & book appointment & list them
        List<Doctor> GetAvailableDoctors(); //available doctor list
        bool BookAppointment(int doctorId,int patientId, DateTime appointmentTime); //booking appoitment for d.id p.id
        List<Appointment> ViewAppointments(); //view appointment in general
    }
}

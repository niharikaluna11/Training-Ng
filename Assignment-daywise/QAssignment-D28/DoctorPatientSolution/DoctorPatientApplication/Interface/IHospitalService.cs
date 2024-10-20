using DoctorPatientApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DoctorPatientApplication.Interface
{
    public interface IHospitalService
    {
        // methods define 
        //patients to list doctors
        ///and book appointment for doctors
        //Please ensure the doctor does not more than one appointment in the same time

        //patient to-do
       /* List<Doctor> GetAvailableDoctors();
        //getting all doctors
        bool BookAppointment(int doctorId, DateTime appointmentTime, int patientId);
        //booking appointment 
        List<Appointment> GetAppointmentsByDoctor(int doctorId);

        List<Appointment> GetAppointmentsByPatient(int patientId);
        bool IsDoctorAvailable(int doctorId, DateTime appointmentTime);*/

    }
}

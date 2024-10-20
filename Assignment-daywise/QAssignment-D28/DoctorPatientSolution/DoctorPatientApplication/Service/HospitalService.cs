using DoctorPatientApplication.Interface;
using DoctorPatientApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientApplication.Service
{
    public class HospitalService : IHospitalService
    {


        public HospitalService()
        {


        }
      
        //listing all doctors 
        public List<Doctor> GetAvailableDoctors()
        {
            // return doctors;
            throw new NotImplementedException();
        }

        public bool IsDoctorAvailable(int doctorId, DateTime appointmentTime)
        {
            throw new NotImplementedException();
        }
        public bool BookAppointment(int doctorId, DateTime appointmentTime, int patientId)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentsByPatient(int patientId)
        {
            throw new NotImplementedException();
        }

    }
}

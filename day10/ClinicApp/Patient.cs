using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public class Patient :  Person, IPatient
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool BookAppointment(int doctorId, DateTime appointmentTime)
        {
            // Check if the doctor exists and if the appointment time is valid
            Doctor doctor = ClinicData.Doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return false;
            }

            // Check if the appointment time is available
            if (ClinicData.Appointments.Any(a => a.DoctorId == doctorId && a.AppointmentTime == appointmentTime))
            {
                Console.WriteLine("Appointment slot is already taken.");
                return false;
            }

            // Book the appointment
            Appointment appointment = new Appointment
            {
                Id = ClinicData.Appointments.Count + 1, // Just an example; you should use a more reliable ID generator
                PatientId = this.Id,
                DoctorId = doctorId,
                AppointmentTime = appointmentTime
            };

            ClinicData.Appointments.Add(appointment);
            Console.WriteLine("Appointment successfully booked!");
            return true;
        }

        public List<Appointment> ViewAppointments()
        {
            return ClinicData.Appointments.Where(a => a.PatientId == this.Id).ToList();
        }

        public List<Doctor> GetAvailableDoctors()
        {
            return ClinicData.Doctors;
        }
     

      
    }
}

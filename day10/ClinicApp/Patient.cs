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

        public bool BookAppointment(int doctorId,int patientId, DateTime appointmentTime)
        {

            try
            { // Check if the doctor exists
                Doctor doctor = ClinicData.Doctors.FirstOrDefault(d => d.Id == doctorId);
                Patient patient = ClinicData.Patients.FirstOrDefault(p => p.Id == patientId);
                if (doctor == null)
                {
                    Console.WriteLine("Doctor not found.");
                    return false;
                }
                if (patient == null)
                {
                    Console.WriteLine("Patient not found.");
                    return false;
                }

                // Check if the appointment time is already taken by the doctor or the patient
                if (ClinicData.Appointments.Any(a => a.DoctorId == doctorId && a.AppointmentTime == appointmentTime))
                {
                    Console.WriteLine("Doctor already has an appointment at this time.");
                    return false;
                }

                if (ClinicData.Appointments.Any(a => a.PatientId == patientId && a.AppointmentTime == appointmentTime))
                {
                    Console.WriteLine("Patient already has an appointment with another doctor at this time.");
                    return false;
                }

                // Book the appointment
                Appointment appointment = new Appointment
                {
                    Id = ClinicData.Appointments.Count + 1, // Replace with a better ID generator in production
                    PatientId = patientId,
                    DoctorId = doctorId,
                    AppointmentTime = appointmentTime
                };

                ClinicData.Appointments.Add(appointment);
                Console.WriteLine("Appointment successfully booked!");
                return true;
            }
            catch (Exception ex)
            { Console.WriteLine(ex.ToString()); 
                return false; }
           
        }

        public List<Appointment> ViewAppointments() //only that person appointmentss 
        {
            return ClinicData.Appointments.Where(a => a.PatientId == this.Id).ToList();

        }


        public List<Doctor> GetAvailableDoctors()
        {
            return ClinicData.Doctors;
        }
     

      
    }
}

using ClinicAppliScation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public class Appointment : IAppointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public TimeSpan Duration { get; private set; } = TimeSpan.FromMinutes(40);

        public bool CheckAppointmentOverlap(int doctorId, DateTime appointmentTime)
        {
            // Check for overlap with existing appointments for the same doctor
            return ClinicData.Appointments.Any(a => a.DoctorId == doctorId &&
                (a.AppointmentTime < appointmentTime.Add(Duration) && a.AppointmentTime.Add(a.Duration) > appointmentTime));
        }

        public bool IsFutureAppointment()
        {
            return AppointmentTime > DateTime.Now;
        }
    }
}

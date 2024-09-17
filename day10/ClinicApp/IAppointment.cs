using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppliScation
{
    public interface IAppointment 
    {
        int Id { get; set; }

        //foreign key
        int DoctorId { get; set; }
        int PatientId { get; set; }

        DateTime AppointmentTime { get; set; }
        TimeSpan Duration { get; }

        //aappointment tym & duration

        //check appointment & see future available appointment


        bool CheckAppointmentOverlap(int doctorId, DateTime appointmentTime);
        bool IsFutureAppointment();
    }
}

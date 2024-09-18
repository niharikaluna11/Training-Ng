using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public interface IPatient : IPerson
    {
        //doctor list & book appointment & list them
        List<Doctor> GetAvailableDoctors();
        bool BookAppointment(int doctorId,int patientId, DateTime appointmentTime);
        List<Appointment> ViewAppointments();
    }
}

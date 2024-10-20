using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientApplication.Models
{

    /*   DoctorID(Primary Key)
       DoctorName
       Specialization
       Email
       PhoneNumber
       */
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<DoctorPatient> DoctorPatients { get; set; }


    }
}

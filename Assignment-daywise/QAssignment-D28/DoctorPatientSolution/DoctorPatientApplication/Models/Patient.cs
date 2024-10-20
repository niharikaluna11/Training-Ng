using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientApplication.Models
{
    /*        PatientID(Primary Key)
               PatientName
               Email
               PhoneNumber
       */
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Email { get; set;} = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<DoctorPatient> DoctorPatients { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientApplication.Models
{
    /* AppointmentID(Primary Key)
      AppointmentDate
      Status(e.g., Completed, Pending, Canceled)
      Other relevant details*/

    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

      public DateTime AppointmentDate { get; set; }
      public string Status { get; set; }

        // as 1 appointment only
        // 1 doc many appointment
        // 1 appointment 1 doctor
        // so only foreign keyof doctor and pateint

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor doctor { get; set; }
    }
}

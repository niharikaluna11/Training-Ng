using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorPatientApplication.Models
{
    public class DoctorPatient
    {


        /*DoctorPatientID(Primary Key)
        DoctorID(Foreign Key referencing Doctor)
        PatientID(Foreign Key referencing Patient)
        AppointmentID(Foreign Key referencing Appointment)*/


        [Key]//annotation for primary key
        public int DPONumber { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor doctor { get; set; }

        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient patient { get; set; }




    }
}

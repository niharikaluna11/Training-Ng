using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicAppliScation
{
    internal interface IAppointment
    {
        int id { get; set; }

        //foreign key
        int DoctorId { get; set; }
        int PatientId { get; set; }

        //aappointment tym & duration

        //check appointment & see future available appointment
    }
}

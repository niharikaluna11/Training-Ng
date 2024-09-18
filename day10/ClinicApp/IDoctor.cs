using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public interface IDoctor : IPerson //doctor is a person
    {
        //patient list & appointment list
        List<Patient> ViewPatients(); 

        List<Appointment> ViewAppointments();
    }
}

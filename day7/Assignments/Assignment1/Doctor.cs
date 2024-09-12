using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Doctor
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int ExperienceYears { get; set; }

        public static List<Doctor> doctorList = new List<Doctor>();

        public Doctor(string name, string specialty, int experienceYears)
        {
            Name = name;
            Specialty = specialty;
            ExperienceYears = experienceYears;
        }
        public static void PrintAllDoctors()
        {
            foreach (var doctor in doctorList)
            {
                if (doctor != null)
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine(" ::               Doctor Details :-                      ::   ");
                    Console.WriteLine($" Name                  : {doctor.Name}");
                    Console.WriteLine($" Speciality            : {doctor.Specialty}");
                    Console.WriteLine($" Experience in Years   : {doctor.ExperienceYears:Years}");
                    Console.WriteLine("---------------------------------------------------------------");
                    
                }
            }
        }
    }
}

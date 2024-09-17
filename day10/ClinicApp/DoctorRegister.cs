using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClinicApplication
{
    public class DoctorRegister
    {
        public void RegisterDoctor()
        {
           
            Console.WriteLine("Registering as a Doctor");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Specialization: ");
            string specialization = Console.ReadLine();


            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();


            Console.Write("Enter User name: ");
            string uname = Console.ReadLine();


            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

             Doctor newDoctor = new Doctor()
            {
                Id = ClinicData.Doctors.Count + 1,
                Name = name,
                Specialization=specialization,
                PhoneNumber = phoneNumber,
                UserName = uname,
                Email = email,
                Password = password
            };

            ClinicData.Doctors.Add(newDoctor);
            

            Console.WriteLine("Doctor registered successfully!");
            
        }

        public Doctor LoginDoctor()
        {
            

            Console.WriteLine("Doctor Login");

            Console.Write("Enter User Name: ");
            string uname = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // Find doctor in the ClinicData
            Doctor doctor = ClinicData.Doctors
                .FirstOrDefault(d => d.UserName == uname && d.Password == password);

            if (doctor != null)
            {
                Console.WriteLine("Login successful!");
                return doctor;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                return null;
            }
           
        }
    }
}

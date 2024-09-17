using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public class PatientRegister
    {
        public void RegisterPatient()
        {
            
            Console.WriteLine("Registering as a Patient");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();


            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();


            Console.Write("Enter User name: ");
            string uname = Console.ReadLine();


            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Patient newPatient = new Patient
            {
                Id = ClinicData.Patients.Count + 1,  // Auto-generate a new ID
                Name = name,
                PhoneNumber = phoneNumber,
                UserName = uname,
                Email = email,
                Password = password
            };

            // Add the new patient to the Patients list
            ClinicData.Patients.Add(newPatient);

            Console.WriteLine("Patient registered successfully!");
            
        }

        public Patient LoginPatient()
        {
            
            Console.WriteLine("Patient Login");

            Console.Write("Enter User Name: ");
            string uname = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Patient patient =  ClinicData.Patients
                .FirstOrDefault(d => d.UserName == uname && d.Password == password);

            if (patient != null)
            {
                Console.WriteLine("Login successful!");
                return patient;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                return null;
            }
            
        }
    }

}

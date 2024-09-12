using System;
using System.Xml.Linq;

namespace Assignment1
{
    internal class Program
    {
        //Create an array of doctors and print the details
        public static void InitializeDoctors()
        {
            bool addMoreDoctors = true;

            while (addMoreDoctors)
            {
                Console.WriteLine("Enter the doctor details :-");
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Specialty:");
                string specialty = Console.ReadLine();
                Console.WriteLine("Enter Experience in years (only integer):");
                int experience = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("--------------------------------------------------------------");

                Doctor doctor = new Doctor(name, specialty, experience);
                Doctor.doctorList.Add(doctor);

                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Do you want to add another doctor? (y/n)");
                string response = Console.ReadLine();
                if (response.ToLower() != "y")
                {
                    addMoreDoctors = false;
                }
              
            }


            }

        
      
        static void Main(string[] args)
        {

            // Initialize doctors array
            InitializeDoctors();

            // Print the details of all doctors
            Doctor.PrintAllDoctors();

            Console.ReadKey();
        }
    }
}

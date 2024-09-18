using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClinicApplication
{
    public class PatientRegister
    {
        static string GetValidName(string prompt)
        {
            string name;
            while (true)
            {
                Console.Write($"{prompt} ");
                name = Console.ReadLine();
                if (IsValidName(name))
                    return name;
                Console.WriteLine("Invalid format. Name must be in 'first last' format.");
            }
        }

        // Method to check if the name is in "first last" format
        static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z]+ [a-zA-Z]+$");
        }

    
        // Method for validating phone numbers (ensuring it's a number and exactly 10 digits long)
        static string GetValidPhoneNumber(string prompt)
        {
            string phoneNumber;
            long phoneNumberValue;
            while (true)
            {
                Console.Write($"{prompt} ");
                phoneNumber = Console.ReadLine();
                if (long.TryParse(phoneNumber, out phoneNumberValue) && phoneNumber.Length == 10)
                    return phoneNumber;
                Console.WriteLine("Phone number must be exactly 10 digits and numeric.");
            }
        }

        // Method for validating email (basic validation)
        static string GetValidEmail(string prompt)
        {
            string email;
            while (true)
            {
                Console.Write($"{prompt} ");
                email = Console.ReadLine();
                if (IsValidEmail(email))
                    return email;
                Console.WriteLine("Invalid email format. Please use 'example@gmail.com'.");
            }
        }

        // Method to check if the email is in valid format
        static bool IsValidEmail(string email)
        {
            // Basic email validation using regex
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        static string GetValidStringInput(string prompt, string errorMessage)
        {
            string input;
            while (true)
            {
                Console.Write($"{prompt} ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
                    return input;
                Console.WriteLine(errorMessage);
            }
        }
        public void RegisterPatient()
        {
            try
            {
                Console.WriteLine("Registering as a Patient");

                // Name input and validation
                string name = GetValidName("Enter Name (first-name last-name):");

                // Phone number input and validation
                string phoneNumber = GetValidPhoneNumber("Enter Phone Number:");

                // Email input and validation
                string email = GetValidEmail("Enter Email:");

                // Username input and validation
                string uname = GetValidStringInput("Enter Username:", "Username cannot be empty.");

                // Password input and validation
                string password = GetValidStringInput("Enter Password:", "Password cannot be empty.");


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
            catch (Exception ex)
            {
                Console.WriteLine($"error occured: {ex.Message}");
            }
          
            
        }

        public Patient LoginPatient()
        {
            try
            {
                Console.WriteLine("Patient Login");
                string uname;
                string password;

               
                Console.Write("Enter User Name: ");
                uname = Console.ReadLine();

                Console.Write("Enter Password: ");
                password = Console.ReadLine();
              

                Patient patient = ClinicData.Patients
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
            catch (Exception ex)
            {
                return null;
                Console.WriteLine($"An error occured: {ex.Message}");

            }


        }
    }

}

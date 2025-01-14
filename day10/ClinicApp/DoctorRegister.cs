﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClinicApplication
{
    public class DoctorRegister
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

        // Method for validating specialization (contains only characters)
        public static string GetValidSpecialization(string prompt)
        {
            string specialization;
            while (true)
            {
                Console.Write($"{prompt} ");
                specialization = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(specialization) && Regex.IsMatch(specialization, @"^[a-zA-Z]+$"))
                    return specialization;
                Console.WriteLine("Specialization must contain only letters.");
            }
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

            public void RegisterDoctor()
        {
            try
            {
                Console.WriteLine("Registering as a Doctor");

                // Name input and validation
                string name = GetValidName("Enter Name (first-name last-name):");

                // Specialization input and validation
                string specialization = GetValidSpecialization("Enter Specialization:");

                // Phone number input and validation
                string phoneNumber = GetValidPhoneNumber("Enter Phone Number:");

                // Email input and validation
                string email = GetValidEmail("Enter Email:");

                // Username input and validation
                string uname = GetValidStringInput("Enter Username:", "Username cannot be empty.");

                // Password input and validation
                string password = GetValidStringInput("Enter Password:", "Password cannot be empty.");


                Doctor newDoctor = new Doctor()
                {
                    Id = ClinicData.Doctors.Count + 1,
                    Name = name,
                    Specialization = specialization,
                    PhoneNumber = phoneNumber,
                    UserName = uname,
                    Email = email,
                    Password = password
                };

                ClinicData.Doctors.Add(newDoctor);


                Console.WriteLine("Doctor registered successfully!");
            }
            catch (Exception ex)
            {
                //return null;
                Console.WriteLine($"An error occured: {ex.Message}");
            }
         
            
        }

        public Doctor LoginDoctor()
        {
            try
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
            catch (Exception ex)
            {
                return null;
                Console.WriteLine($"An error occured: {ex.Message}");

            }
           
           
        }
    }
}

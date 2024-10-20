﻿using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace ClinicApplication
{
    public class Program
    {
        public static void FetchDoctors()
        {
            Console.WriteLine("Fetching doctors in the background...");
            Thread.Sleep(2000); // Simulate a delay of 5 seconds for fetching doctors
            Console.WriteLine("Doctors fetched successfully.");
        }

        //--------------------------------------------------------------------------------------------------
        //main menu for all the functioning
        void PrintMenu()
        {
            // printing the main menu
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello, Welcome To The Prestige Clinic!");
            Console.WriteLine("Hello ! Are you ?");
            Console.WriteLine("1 - DOCTOR :)");
            Console.WriteLine("2- PATIENT :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
            Console.ResetColor();

        }
        void DoctorPrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello Doctor !");
            Console.WriteLine("1 -REGISTER YOURSELF:)");
            Console.WriteLine("2- LOGIN :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
            Console.ResetColor();
        }
        void DoctorToDoMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDoctor Menu:");
            Console.WriteLine("1. View All Patients");
            Console.WriteLine("2. View My Patients");
            Console.WriteLine("3. View  Appointments (ALL TIME) || (FUTURE ONLY)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
            Console.ResetColor();
        }
        void PatientToDoMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n Patients Menu:");
            Console.WriteLine("1. View All Doctors");
            Console.WriteLine("2. View My Appointments");
            Console.WriteLine("3. Book My Appointments");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
            Console.ResetColor();
        }
        void PatientPrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hello Patient !");
            Console.WriteLine("1 -REGISTER YOURSELF:)");
            Console.WriteLine("2- LOGIN :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
            Console.ResetColor();
        }

        //------------------------------------------------------------------------------------------------

        //Doctor's main menu - register & login done
        void DoctorMainMenu()
        {
            Program program = new Program();
            int choice = -1;
            
            DoctorRegister doctorRegister = new DoctorRegister();
             
           
            while (choice != 0)
            {
                program.DoctorPrintMenu();

               
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }

                // Handle the user choice
                switch (choice)
                {
                    case 1:
                        //Console.WriteLine("REGISTER!");
                        //program.RegisterDoctor();
                        doctorRegister.RegisterDoctor();
                        Doctor doctor = doctorRegister.LoginDoctor();
                        if (doctor != null)  // If login is successful
                        {
                            Console.WriteLine($"Hello Doctor {doctor.Name}!");
                            DoctorTodo(doctor);
                        }
                        else
                        {
                            Console.WriteLine("Login failed. Exiting.");
                        }
                        break;
                    case 2:
                        //Console.WriteLine("LOGIN!");
                        Doctor doctorr = doctorRegister.LoginDoctor();
                        //now ask him more things like
                        ////for doctor: view patient of his & all & appointment
                        if (doctorr != null)  // If login is successful
                        {
                           
                            Console.WriteLine($"Hello Doctor {doctorr.Name}!");
                           DoctorTodo(doctorr);
                        }
                        else
                        {
                            Console.WriteLine("Login failed. Exiting.");
                        }
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }
        }

       

        //Doctor to do menu- 
        // View Patients done :)
        //view his own patients 
        //View Appointments (all
        //& future only)
        private static void DoctorTodo(Doctor doctor)
        {
            
            //Console.Write("Select an option: ");
            int option = -1;

            while (option != 0)
            {

                new Program().DoctorToDoMenu();

                while (!int.TryParse(Console.ReadLine(), out option) || (option < 0 || option > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }

                // Handle the user choice
                switch (option)
                {
                    case 1:
                        //view all patientss
                        List<Patient> allPatients = doctor.ViewPatients();
                        if (allPatients.Count == 0)
                        {
                            Console.WriteLine("No patients found.");
                        }
                        else
                        {
                            Console.WriteLine("ALL Patients :)");
                            foreach (var patient in allPatients)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine($"Patient ID: {patient.Id}:-\n \t Name: {patient.Name} \n \t Email: {patient.Email}  \n \t Number: {patient.PhoneNumber}");
                                Console.WriteLine("---------------------------------------------------");

                            }
                        }
                        break;
                    case 2:
                        //viewmypatients
                        //only mine
                        // View my patients (only for the logged-in doctor)
                        List<Patient> allPatientsOfDoctor = doctor.ViewPatients();

                        if (allPatientsOfDoctor.Count == 0)
                        {
                            Console.WriteLine("No patients found.");
                        }
                        else
                        {
                            Console.WriteLine($"Patients of Dr. {doctor.Name}:");

                            foreach (var patient in allPatientsOfDoctor)
                            {
                                // Find the appointment for this patient with the current doctor
                                List<Appointment> appointmentss = doctor.ViewAppointments()
                                    .Where(a => a.DoctorId == doctor.Id) // Filter appointments for the current patient
                                    .ToList();

                                 Console.WriteLine($"Patient ID: {patient.Id}:-\n \t Name: {patient.Name} \n \t Email: {patient.Email}  \n \t Number: {patient.PhoneNumber}");
                                                                     
                            }
                        }
                       

                        break;

                    case 3:
                        //both
                        //all & future only
                        List<Appointment> appointments = doctor.ViewAppointments()
                                        .Where(a => a.DoctorId == doctor.Id)
                                        .ToList();  // Filter appointments for the current doctor
                        List<Patient> allPatientsDoctor = doctor.ViewPatients();  // Assuming this already retrieves patients relevant to the doctor

                        if (appointments.Count > 0)
                        {
                            Console.WriteLine("\nYour Appointments:");
                            foreach (var appointment in appointments)
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine($"Appointment with Patient :- ID {appointment.PatientId} on {appointment.AppointmentTime}");

                                // Find the patient for the current appointment
                                var patient = allPatientsDoctor.FirstOrDefault(p => p.Id == appointment.PatientId);
                                if (patient != null)
                                {
                                    Console.WriteLine($"Patient Name : {patient.Name},\n\t Email: {patient.Email},\n\t Number: {patient.PhoneNumber}");
                                }

                                Console.WriteLine("---------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No appointments found for this doctor.");
                        }


                        break;
                    
                    case 5:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }
           
        }


        //--------------------------------------------------------------------------------------------------------

        //Patient's main menu - register & login done
        void PatientMainMenu()
        {
            Program program = new Program();
            int choice = -1;

            PatientRegister patientRegister = new PatientRegister();

            while (choice != 0)
            {
                program.PatientPrintMenu();


                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }


                switch (choice)
                {
                    case 1:
                        //Console.WriteLine("REGISTER!");
                        //program.RegisterDoctor();
                        patientRegister.RegisterPatient();
                        Patient patient = patientRegister.LoginPatient();
                        if (patient != null)  // If login is successful
                        {
                            Console.WriteLine($"Hello patient {patient.Name}!");
                            PatientTodo(patient);
                        }
                        else
                        {
                            Console.WriteLine("Login failed. Exiting.");
                        }
                        break;
                    case 2:
                        //Console.WriteLine("LOGIN!");
                        Patient patientt = patientRegister.LoginPatient();
                        //now ask him more things like
                        ////for patient: doctor list & book appointment & list them
                        if (patientt != null)  // If login is successful
                        {
                            Console.WriteLine($"Hello patient {patientt.Name}!");
                            PatientTodo(patientt);
                        }
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }
        }


        //Patient to do menu- 
        //view doctors done :)
        //Book Appointment
        //View Appointment
        private static void PatientTodo(Patient patient)
        {

            //Console.Write("Select an option: ");
            int option = -1;

            while (option != 0)
            {


                new Program().PatientToDoMenu();

                while (!int.TryParse(Console.ReadLine(), out option) || (option < 0 || option > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }

                // Handle the user choice
                switch (option)
                {
                    case 1:
                        //GetAvailableDoctors
                        List<Doctor> allDoctors = patient.GetAvailableDoctors();
                        if (allDoctors.Count == 0)
                        {
                            Console.WriteLine("No Doctors found.");
                        }
                        else
                        {
                            foreach (var doctor in allDoctors)
                            {
                                //doctor listing
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine($"Doctor ID: {doctor.Id}:-\n \t Name: {doctor.Name} \n \t Email: {doctor.Email} \n \t E Number: {doctor.PhoneNumber},  \n \t ESpecialization :{doctor.Specialization}");
                                Console.WriteLine("---------------------------------------------------");
                            }
                        }
                        break;
                    case 2:
                        //view appointments
                        //all & future only
                        List<Appointment> appointments = patient.ViewAppointments();
                        List<Doctor> doctors = patient.GetAvailableDoctors();
                        if (appointments.Count > 0)
                        {
                            Console.WriteLine("\nYour Appointments:");
                            foreach (var appointment in appointments )
                            {
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine($"Appointment with Doctor :- ID {appointment.DoctorId} \n \t \t naming  on {appointment.AppointmentTime}");
                                foreach (var doctor in doctors)
                                {
                                    if(doctor.Id == appointment.DoctorId)
                                    {
                                        Console.WriteLine($"Doctor Name : {doctor.Name} ,\n   \t Email :{doctor.Email} ,\n  \t Number :{doctor.PhoneNumber}");

                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no appointments.");
                        }
                        break;

                    case 3:
                        //listing first
                        //booking thn

                        List<Doctor> allDoctorrs = patient.GetAvailableDoctors();

                        Console.Write("These are available Doctor Specializationss:\n ");
                        int count = 1;
                        foreach (var speciality in allDoctorrs.Select(e=>e.Specialization).Distinct())
                        {
                                //specialization listing
                                Console.WriteLine($"{count}. {speciality}");
                                count++;   
                         }

                        // Assuming DoctorRegister.GetValidSpecialization is static
                        string specializationInput = "Enter specialization";
                        string specialization = DoctorRegister.GetValidSpecialization(specializationInput);


                        if (allDoctorrs.Count == 0)
                        {
                            Console.WriteLine("No Doctors found.");
                        }
                        else
                        {
                            foreach (var doctor in allDoctorrs)
                            {
                                if(doctor.Specialization == specialization)
                                {
                                    //doctor listing
                                    Console.WriteLine("---------------------------------------------------");
                                    Console.WriteLine($"Doctor ID: {doctor.Id} :: \t Name: {doctor.Name} \n ");
                                    Console.WriteLine("---------------------------------------------------");
                                }
                               
                            }
                        }

                       
                        Console.Write("Enter Doctor ID: ");
                        string input = Console.ReadLine();
                        int doctorId;

                        if (int.TryParse(input, out doctorId))
                        {
                            // Successfully parsed the Doctor ID
                            Console.WriteLine($"Doctor ID entered: {doctorId}");
                            // Proceed with the rest of your logic
                        }
                        else
                        {
                            // Invalid input, handle accordingly
                            Console.WriteLine("Invalid Doctor ID. Please enter a valid number.");
                        }

                        Console.Write("Enter Appointment Date and Time (yyyy-mm-dd hh:mm): ");
                        DateTime appointmentTime;
                        if (!DateTime.TryParse(Console.ReadLine(), out appointmentTime))
                        {
                            Console.WriteLine("Invalid date format.");
                            return;
                        }

                        patient.BookAppointment(doctorId,patient.Id, appointmentTime);

                        //book appointment

                        break;

                    case 5:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            }

        }

       //-------------------------------------------------------------------------------------------------
       //main code
       //main menu of 'prestige clinic :)'
       static void Main(string[] args)
        {
            
            Program program = new Program();
            //InitializeData();

            // Start a background thread to fetch doctor data
            Thread fetchDoctorsThread = new Thread(FetchDoctors);
            fetchDoctorsThread.Start();

            int choice = -1;
            while (choice != 0)
            {
                program.PrintMenu();
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }
                switch (choice)
                {
                    case 1:
                        //Console.WriteLine("You are a doctor!");
                        program.DoctorMainMenu();
                        break;
                    case 2:
                        //Console.WriteLine("You are a patient!");
                        program.PatientMainMenu();
                        break;
                    case 5:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Exiting the application.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            // Ensure the background thread finishes before exiting the application
            fetchDoctorsThread.Join();

            Console.WriteLine("Application exited. All tasks completed.");
            Console.ReadKey();
            //Console.ReadKey();
            ////end of main
        }

    } // end of program class
}

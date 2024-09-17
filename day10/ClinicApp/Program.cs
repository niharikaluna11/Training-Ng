namespace ClinicApplication
{
    internal class Program
    {
         void PrintMenu()
        {
            // printing the main menu
            Console.WriteLine("Hello, Welcome To The Prestige Clinic!");
            Console.WriteLine("Hello ! Are you ?");
            Console.WriteLine("1 - DOCTOR :)");
            Console.WriteLine("2- PATIENT :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");

        }

        void DoctorPrintMenu()
        {
            Console.WriteLine("Hello Doctor !");
            Console.WriteLine("1 -REGISTER YOURSELF:)");
            Console.WriteLine("2- LOGIN :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
        }

        void PatientPrintMenu()
        {
            Console.WriteLine("Hello Patient !");
            Console.WriteLine("1 -REGISTER YOURSELF:)");
            Console.WriteLine("2- LOGIN :)");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine("5- Clear Screen");
        }

        void DoctorMainMenu()
        {
            Program program = new Program();
            int choice = -1;

            // Keep looping until the user chooses to exit (option 0)
            while (choice != 0)
            {
                program.DoctorMainMenu();

                // Keep asking until valid input is provided
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 5))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                }

                // Handle the user choice
                switch (choice)
                {
                    case 1:
                        //Console.WriteLine("REGISTER!");

                        break;
                    case 2:
                        //Console.WriteLine("LOGIN!");

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

        static void Main(string[] args)
        {
            Program program = new Program();    
          
            

            int choice = -1;

            // Keep looping until the user chooses to exit (option 0)
            while (choice != 0)
            {
                program.PrintMenu();

                 // Keep asking until valid input is provided
                    while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 5))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid option (0, 1, 2, 5):");
                    }

                    // Handle the user choice
                    switch (choice)
                    {
                        case 1:
                            //Console.WriteLine("You are a doctor!");
                            program.DoctorMainMenu();
                            break;
                        case 2:
                            //Console.WriteLine("You are a patient!");
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

            Console.ReadKey();
            //end of main
        }

    } // end of program class
}

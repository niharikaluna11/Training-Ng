using CustomerApplicationD27.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace CustomerApplicationD27
{
    public class Program
    {
        // main menu
        public void MainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hello, Customer :) ");
                Console.WriteLine("Are you a new or old user?");
                Console.WriteLine("ENTER \n1- Login \n2- Register \n0- Exit\n");
                Console.Write("Enter: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    UserService userService = new UserService();

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("EXITING.....");
                            exit = true;
                            break;
                        case 1:
                            if (userService.Login())
                            {
                                Console.Write("Enter your Customer ID: ");
                                string customerId = Console.ReadLine();
                                CustomerMainMenu(customerId);
                            }
                            break;
                        case 2:
                            Console.WriteLine("REGISTER");
                            userService.Register();

                            if (userService.Login())
                            {
                                Console.Write("Enter your Customer ID: ");
                                string customerId = Console.ReadLine();
                                CustomerMainMenu(customerId);
                            }
                            break;
                        default:
                            Console.WriteLine("Enter correct option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                PauseAndClear();
            }
        }

        // customer main menu
        public void CustomerMainMenu(string customerId)
        {
            bool exit = false;
            CustomerService customerService = new CustomerService();

            

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1) View Previous Orders");
                Console.WriteLine("2) View Order Summary");
                Console.WriteLine("3) View Shipper Details");
                Console.WriteLine("4) Update Password");
                Console.WriteLine("5) Back to Main Menu");
                Console.WriteLine("6) Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            customerService.ViewPreviousOrders(customerId);
                            break;
                        case 2:
                            
                                customerService.ViewOrderSummary(customerId);
                           
                            break;
                        case 3:
                         
                                customerService.ViewShipperDetails(customerId);
                           
                            break;
                        case 4:
                            UserService userService = new UserService();
                            userService.UpdatePassword();
                            break;
                        case 5:
                            exit = true;
                            break;
                        case 6:
                            Console.WriteLine("Exiting...");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
                PauseAndClear();
            }
        }

        public void PauseAndClear()
        {

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        // program 
        public Program()
        {
            MainMenu();  //calling main menu
        }

        static void Main(string[] args)
        {
            Program program = new Program();
        }
    }
}

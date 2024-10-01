using System.Xml.Linq;

namespace BankApplication
{
    
        internal class Program
        {
            // Main program logic
            public void SavingAccountfunction()
            {
                Console.Write("Enter Account Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Account Number: ");
                long accountNumber = Convert.ToInt64(Console.ReadLine());

                SavingAccount savingAccount = new SavingAccount(accountNumber, name);
                savingAccount.DisplayBalance();

                Console.WriteLine("Login to your account");
                SavingAccountWorking(savingAccount);
            }

            public void SavingAccountWorking(SavingAccount savingAccount)
            {
                while (true)
                {
                    savingAccount.DisplayBalance();

                    Console.WriteLine("Choose an operation: \n 1. Deposit \n 2. Withdraw \n 3. Exit");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.Write("Enter deposit amount:");
                            double depositAmount = Convert.ToDouble(Console.ReadLine());
                            savingAccount.Deposit(depositAmount);
                            break;

                        case 2:
                            Console.Write("Enter withdrawal amount:");
                            double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
                            savingAccount.Withdrawl(withdrawalAmount);
                            break;

                        case 3:
                            Console.WriteLine("Returning to the main menu...");
                            return;  // Exit the function and return to the menu

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
            }

            public void CurrentAccountfunction()
            {
                Console.WriteLine("Enter Account Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Account Number");
                long accountNumber = Convert.ToInt64(Console.ReadLine());

                CurrentAccount currentAccount = new CurrentAccount(accountNumber, name);
                Console.WriteLine("Enter deposit amount:");
                double depositAmount = Convert.ToDouble(Console.ReadLine());
                currentAccount.Deposit(depositAmount);

                currentAccount.DisplayBalance();
                Console.WriteLine("Login to your account");
                CurrentAccountWorking(currentAccount);
            }

            public void CurrentAccountWorking(CurrentAccount currentAccount)
            {
                while (true)
                {
                    currentAccount.DisplayBalance();

                    Console.WriteLine("Choose an operation: \n 1. Deposit \n 2. Withdraw \n 3. Exit");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Enter deposit amount:");
                            double depositAmount = Convert.ToDouble(Console.ReadLine());
                            currentAccount.Deposit(depositAmount);
                            break;

                        case 2:
                            Console.WriteLine("Enter withdrawal amount:");
                            double withdrawalAmount = Convert.ToDouble(Console.ReadLine());
                            currentAccount.Withdrawl(withdrawalAmount);
                            break;

                        case 3:
                            Console.WriteLine("Returning to the main menu...");
                            return;  // Exit the function and return to the menu

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
            }

            public void PrintMenu()
            {
                int option;
                do
                {
                    Console.WriteLine("Welcome to the Bank");
                    Console.WriteLine("1. Saving Account \n2. Current Account \n0. Exit");
                    Console.Write("Enter: ");
                    option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            SavingAccountfunction();
                            break;
                        case 2:
                            CurrentAccountfunction();
                            break;
                        
                        case 0:
                            Console.WriteLine("Exiting application...");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                } while (option != 0);
            }

            public Program()
            {
                PrintMenu();
            }

            static void Main(string[] args)
            {
                Program program = new Program();
                Console.ReadKey();
            }
        }
}

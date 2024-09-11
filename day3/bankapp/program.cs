using System.Security.Principal;

namespace bankapp
{
    internal class Program
    {
        void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the Bank Management System");
            Console.WriteLine(" 1 - Create Normal Account");
            Console.WriteLine(" 2 - Create NRI Account");
            Console.WriteLine(" 3 - Create Salary Account");
            Console.WriteLine(" 4 - Clear Screen");
            Console.WriteLine(" 0 - Exit");
        }

        void MainInteraction()
        {
            int choice;
            do
            {
                PrintMainMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Normal Account :)");
                        normalAcct();
                        break;
                    case 2:
                        Console.WriteLine("NRI Account :)");
                        nriAcct();
                        break;
                    case 3:
                        Console.WriteLine("Salary Account :)");
                        salaryAcct();
                        break;
                    case 4:
                        Console.Clear();
                        break;

                    case 0:
                        Console.WriteLine("Exiting Sytem...");
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            } while (choice != 0);
        }

        private void normalAcct()
        {
            Console.WriteLine("--- Creating Normal Account ---");
            Console.WriteLine("Enter Amount to Deposit (minimum: 5000):");
            double balance = Convert.ToDouble(Console.ReadLine());
            IbankAcc normalAccount = new NormalBankA(balance);
            Console.WriteLine("Normal Account:");
            Console.WriteLine($"Initial Balance: {normalAccount.getBalance()}");

            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool transactionSuccess = normalAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {transactionSuccess}");
            if (transactionSuccess)
            {
                Console.WriteLine($"New Balance after transaction of {transactBalance}: {normalAccount.getBalance()}");
                Console.WriteLine("thankyou :)");
            }
        }

        private void nriAcct()
        {
            Console.WriteLine("--- Creating NRI Account ---");
            Console.WriteLine("Enter Amount to Deposit (minimum: 10000):");
            double balance = Convert.ToDouble(Console.ReadLine());
            IbankAcc nriAccount = new NRIBankA(balance);
            Console.WriteLine("NRI Account:");
            Console.WriteLine($"Initial Balance: {nriAccount.getBalance()}");

            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool transactionSuccess = nriAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {transactionSuccess}");
            if (transactionSuccess)
            {
                Console.WriteLine($"New Balance after transaction of {transactBalance}: {nriAccount.getBalance()}");
                Console.WriteLine("thankyou :)");
            }
        }

        private void salaryAcct()
        {

            Console.WriteLine("--- Creating Salary Account ---");
            Console.WriteLine("Enter Amount to Deposit:");
            double balance = Convert.ToDouble(Console.ReadLine());
            IbankAcc salaryAccount = new SalaryBankA(balance);
            Console.WriteLine("Salary Account:");
            Console.WriteLine($"Initial Balance: {salaryAccount.getBalance()}");

            Console.WriteLine("Enter Amount for Transaction: ");
            double transactBalance = Convert.ToDouble(Console.ReadLine());
            bool transactionSuccess = salaryAccount.MoneyTransaction(transactBalance);
            Console.WriteLine($"Transaction Success: {transactionSuccess}");
            if (transactionSuccess)
            {
                Console.WriteLine($"New Balance after transaction of {transactBalance}: {salaryAccount.getBalance()}");
                Console.WriteLine("thankyou :)");
            }
        }
        static void Main(string[] args)
        {
            var program = new Program();
            program.MainInteraction();
        }
    }
}

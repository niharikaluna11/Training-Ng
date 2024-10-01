namespace BankApplicationNew
{
    internal class Program
    {
         public void PrintMenu()
        {
            int option;
            do
            {
                Console.WriteLine("Welcome to the Bank");
                Console.WriteLine("1. Saving Account \n2. Current Account \n0. Exit");
                Console.Write("Enter: ");
                option = Convert.ToInt32(Console.ReadLine());

                IAccount account = null; //ok
                switch (option)
                {
                    case 1:
                        //SavingAccountfunction();
                        Console.Write("Enter Account Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Account Number: ");
                        long accountNumber = Convert.ToInt64(Console.ReadLine());

                        account = new SavingAccount(accountNumber, name);
                        break;

                    case 2:
                        //CurrentAccountfunction();
                        Console.WriteLine("Enter Account Name");
                        string namee = Console.ReadLine();
                        Console.WriteLine("Enter Account Number");
                        long accountNumbere = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Enter deposit amount:");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        account = new CurrentAccount(accountNumbere, namee);
                        account.Deposit(depositAmount);

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

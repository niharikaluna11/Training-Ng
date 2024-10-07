namespace FactoryPatternApp
{
    class Program
    {
        public Program()
        {
            Console.WriteLine("Welcome to the Bank Account Management System!");
            Console.WriteLine("Please select the type of account you want to create:");
            Console.WriteLine("1. Salary Account");
            Console.WriteLine("2. Saving Account");
            Console.WriteLine("3. Current Account");
            Console.Write("Enter your choice (1/2/3): ");

            int choice = int.Parse(Console.ReadLine());
            CreateAccount(choice);
        }

        private void CreateAccount(int choice)
        {
            BankAccountBuilder accountBuilder = new BankAccountBuilder();
            AccountFactory accountFactory = null;
            string accountHolderName;

            switch (choice)
            {
                case 1:
                    accountFactory = new SalaryAccountFactory();
                    Console.Write("Enter the Account Holder's Name: ");
                    accountHolderName = Console.ReadLine();
                    var salaryAccount = accountBuilder.SetAccountType(accountFactory)
                                                      .SetAccountHolder(accountHolderName)
                                                      .Build();
                    PerformAccountOperations(salaryAccount);
                    break;

                case 2:
                    accountFactory = new SavingAccountFactory();
                    Console.Write("Enter the Account Holder's Name: ");
                    accountHolderName = Console.ReadLine();
                    var savingAccount = accountBuilder.SetAccountType(accountFactory)
                                                      .SetAccountHolder(accountHolderName)
                                                      .Build();
                    PerformAccountOperations(savingAccount);
                    break;

                case 3:
                    accountFactory = new CurrentAccountFactory();
                    Console.Write("Enter the Account Holder's Name: ");
                    accountHolderName = Console.ReadLine();
                    var currentAccount = accountBuilder.SetAccountType(accountFactory)
                                                       .SetAccountHolder(accountHolderName)
                                                       .Build();
                    PerformAccountOperations(currentAccount);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    break;
            }
        }

        private void PerformAccountOperations(IBankAccount account)
        {
            Console.WriteLine("Account created successfully!");
            Console.WriteLine("1. Deposit Money");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Display Account Details");
            Console.WriteLine("Enter your choice (1/2/3): ");

            int operationChoice = int.Parse(Console.ReadLine());

            switch (operationChoice)
            {
                case 1:
                    Console.Write("Enter amount to deposit: ");
                    decimal depositAmount = decimal.Parse(Console.ReadLine());
                    account.Deposit(depositAmount);
                    account.DisplayDetails();
                    break;

                case 2:
                    Console.Write("Enter amount to withdraw: ");
                    decimal withdrawalAmount = decimal.Parse(Console.ReadLine());
                    account.Withdraw(withdrawalAmount);
                    account.DisplayDetails();
                    break;

                case 3:
                    account.DisplayDetails();
                    break;

                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}

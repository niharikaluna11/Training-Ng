using System.Security.Principal;

namespace bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Ibankacc normalAccount = new normalacc(6000M);
            Ibankacc nriAccount = new nriacc(15000M);
            Ibankacc salaryAccount = new salaryacc(10000M);

            // Normal Account Operations
            Console.WriteLine($"Normal Account Balance: {normalAccount.GetBalance()}");
            normalAccount.Deposit(1000M);
            normalAccount.Withdraw(1000M);
            Console.WriteLine($"Normal Account Balance after transactions: {normalAccount.GetBalance()}");

            // NRI Account Operations
            Console.WriteLine($"NRI Account Balance: {nriAccount.GetBalance()}");
            nriAccount.Deposit(2000M);
            nriAccount.Withdraw(1000M);
            Console.WriteLine($"NRI Account Balance after transactions: {nriAccount.GetBalance()}");

            // Salary Account Operations
            Console.WriteLine($"Salary Account Balance: {salaryAccount.GetBalance()}");
            salaryAccount.Deposit(500M);
            salaryAccount.Withdraw(1000M);
            Console.WriteLine($"Salary Account Balance after transactions: {salaryAccount.GetBalance()}");
        }
    }
}

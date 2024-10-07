using System;

namespace FactoryPatternApp
{
    public class CurrentAccount : IBankAccount
    {
        private decimal balance;
        public string AccountHolderName { get; set; }

        public void Deposit(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Deposit amount must be positive.");

                balance += amount;
                Console.WriteLine($"Deposited {amount} to Current Account.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deposit: {ex.Message}");
            }
        }

        public void Withdraw(decimal amount)
        {
            try
            {
                if (amount <= 0)
                    throw new ArgumentException("Withdrawal amount must be positive.");

                if (balance >= amount)
                {
                    balance -= amount;
                    Console.WriteLine($"Withdrawn {amount} from Current Account.");
                }
                else
                {
                    throw new InvalidOperationException("Insufficient balance in Current Account.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during withdrawal: {ex.Message}");
            }
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Current Account: {AccountHolderName}, Balance: {balance}");
        }
    }
}

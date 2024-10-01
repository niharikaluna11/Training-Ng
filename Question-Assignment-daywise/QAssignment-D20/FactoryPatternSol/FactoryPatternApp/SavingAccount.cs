using System;

namespace FactoryPatternApp
{
    public class SavingAccount : IBankAccount
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
                Console.WriteLine($"Deposited {amount} to Saving Account.");
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
                    Console.WriteLine($"Withdrawn {amount} from Saving Account.");
                }
                else
                {
                    throw new InvalidOperationException("Insufficient balance in Saving Account.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during withdrawal: {ex.Message}");
            }
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Saving Account: {AccountHolderName}, Balance: {balance}");
        }
    }
}

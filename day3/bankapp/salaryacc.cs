using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    internal class salaryacc
    {
        private decimal balance;

        public salaryacc(decimal initialBalance)
        {
            balance = initialBalance; // No minimum balance required
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (balance - amount < 0)
            {
                throw new Exception("Insufficient balance.");
            }
            balance -= amount;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public decimal CalculateTransactionCharge(decimal amount)
        {
            return 0; // No transaction charges for Salary Account
        }
    }
}

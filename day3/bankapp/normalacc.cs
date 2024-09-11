using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    internal class normalacc
    {
        private decimal balance;
        private const decimal TransactionChargePercentage = 0.001M; // 0.1%
        private const decimal MinimumBalance = 5000M;

        public normalacc(decimal initialBalance)
        {
            balance = initialBalance >= MinimumBalance ? initialBalance : throw new Exception("Initial balance must be at least Rs. 5000");
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            decimal charge = CalculateTransactionCharge(amount);
            if (balance - amount - charge < MinimumBalance)
            {
                throw new Exception("Cannot withdraw, minimum balance Rs. 5000 must be maintained.");
            }
            balance -= amount + charge;
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public decimal CalculateTransactionCharge(decimal amount)
        {
            return amount * TransactionChargePercentage;
        }
    }
}

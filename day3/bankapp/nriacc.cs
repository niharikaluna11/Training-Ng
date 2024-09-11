using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    internal class nriacc
    {
        private decimal balance;
        private const decimal TransactionChargePercentage = 0.02M; // 2%
        private const decimal MinimumBalance = 10000M;

        public nriacc(decimal initialBalance)
        {
            balance = initialBalance >= MinimumBalance ? initialBalance : throw new Exception("Initial balance must be at least Rs. 10000");
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
                throw new Exception("Cannot withdraw, minimum balance Rs. 10000 must be maintained.");
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

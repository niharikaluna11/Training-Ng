using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    public class NormalBankA : IbankAcc
    {
        public double balance;
        public const double MinBalance = 5000;
        public const double TransactionChargePercent = 0.001; // 0.1%

        public NormalBankA(double initialBalance)
        {
            if (initialBalance < MinBalance)
            {
                balance = MinBalance; // Set to minimum balance if initial balance is less
            }
            else
            {
                balance = initialBalance;
            }
        }

        public bool MoneyTransaction(double amount)
        {
            double transactionCharge = amount * TransactionChargePercent;
            double totalDeduction = amount + transactionCharge;

            if (balance - totalDeduction >= MinBalance)
            {
                balance -= totalDeduction;
                return true;
            }
            Console.WriteLine($"Transaction Failed!! Your Balance is less than Minimum Balance {MinBalance}");
            return false;
        }

        public bool hasMinimumBalance()
        {
            return balance >= MinBalance;
        }

        public double getBalance()
        {
            return balance;
        }
    }
}

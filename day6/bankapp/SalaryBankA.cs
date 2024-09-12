using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp 
{
    public class SalaryBankA : IbankAcc
    {
        double balance;

        public SalaryBankA(double amount)
        {
            balance = amount;
        }

        public bool MoneyTransaction(double amount)
        {
            if (balance - amount >= 0)
            {
                balance -= amount;
                return true;
            }
            return false;
        }

        public bool hasMinimumBalance()
        {
            return true; // No minimum balance requirement
        }

        public double getBalance()
        {
            return balance;
        }
    
}
}

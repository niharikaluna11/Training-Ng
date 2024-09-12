using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    internal interface IbankAcc
    {
        bool MoneyTransaction(double amount); // Method to perform a transaction
        bool hasMinimumBalance(); // Method to check if the account has the minimum required balance
        double getBalance(); // Method to get the current balance of the account

    }
}

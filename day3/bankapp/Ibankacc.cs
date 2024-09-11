using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bankapp
{
    internal interface Ibankacc
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        decimal GetBalance();
        decimal CalculateTransactionCharge(decimal amount);

    }
}

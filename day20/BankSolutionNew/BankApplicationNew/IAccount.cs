using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationNew
{
    public interface IAccount
    {

        void Withdrawl(double amount);

         void Deposit(double amount);
         void DisplayBalance();

    }
}

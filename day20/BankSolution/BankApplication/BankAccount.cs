using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
    // Implement the POC for Abstract class using Accounts class example

{
    public abstract class BankAccount //main class
    {
        public long AccountNumber { get; set; }
        public string AccountName { get; set; }

        public double Balance {  get; set; }
        public BankAccount(long accountNumber,string accountName) 
            //constructor 
        { 
            AccountName=accountName;
            AccountNumber=accountNumber;
        }

        public abstract void Withdrawl(double amount);

        public abstract void Deposit(double amount);
        public virtual void DisplayBalance()
        {
            Console.WriteLine($"Account Number is { AccountNumber} \nAccount Name is {AccountName} \nBalance is {Balance}");
        }

        //saving aur current account
        ///withdrwal & deposit, displaybalance -methods 
        ///

    }
}

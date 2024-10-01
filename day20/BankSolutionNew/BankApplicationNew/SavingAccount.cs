using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplicationNew
{
    public class SavingAccount : BankAccount
    {
        public double interest = 0.05;
        public SavingAccount(long accountNumber, string accountHolder) : base(accountNumber, accountHolder) { }
        //called the constructor of the abstract class

        public override void Deposit(double depositAamount)
        {
            Balance += depositAamount;
            Console.WriteLine($"You have deposit {depositAamount}\n And your Current Balance is {Balance}");
        }

        public override void Withdrawl(double withdrawlAmount)
        {
            if (Balance - withdrawlAmount <= 0)
            {
                Console.WriteLine("You have insufficient Balance ");
            }
            else
            {
                Balance -= withdrawlAmount;
                Console.WriteLine($"You have withdrawn {withdrawlAmount} from saving account. And Current balance: {Balance}");
            }
        }

        public void AddInterest()
        {
            Balance += Balance * interest;
            Console.WriteLine($"Interest addded. New Balance: {Balance}");
        }

    }
}

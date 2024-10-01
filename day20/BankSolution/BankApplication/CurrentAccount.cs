using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class CurrentAccount : BankAccount
    {
        public CurrentAccount(long accountNumber, string accountHolder) : base(accountNumber, accountHolder) { }
        //called the constructor of the abstract class

        public override void Deposit(double depositAamount)
        {
            try
            {
                if(depositAamount > 200000)
                {
                    throw new CuurentAccountLimitException("Deposit Amount Limit Reached");
                }
                Balance += depositAamount;
                Console.WriteLine($"You have deposit {depositAamount}\n And your Current Balance is {Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void Withdrawl(double withdrawlAmount)
        {
            try
            {
                if (Balance - withdrawlAmount <= 0)
                {
                    throw new CuurentAccountLimitException("You have insufficient Balance");
                    //Console.WriteLine("Y ");
                }
                else
                {
                    Balance -= withdrawlAmount;
                    Console.WriteLine($"You have withdrawn {withdrawlAmount} from saving account");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}

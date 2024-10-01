using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternApp
{
    public abstract class AccountFactory
    {
        public abstract IBankAccount CreateAccount();
    }
    public class SalaryAccountFactory : AccountFactory
    {
        public override IBankAccount CreateAccount()
        {
            return new SalaryAccount();
        }
    }

    public class SavingAccountFactory : AccountFactory
    {
        public override IBankAccount CreateAccount()
        {
            return new SavingAccount();
        }
    }

    public class CurrentAccountFactory : AccountFactory
    {
        public override IBankAccount CreateAccount()
        {
            return new CurrentAccount();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternApp
{
    public class BankAccountBuilder
    {
        private IBankAccount _account;
        public BankAccountBuilder SetAccountType(AccountFactory factory)
        {
            _account = factory.CreateAccount();
            return this;
        }

        public BankAccountBuilder SetAccountHolder(string name)
        {
            if (_account is SalaryAccount salaryAccount)
            {
                salaryAccount.AccountHolderName = name;
            }
            else if (_account is SavingAccount savingAccount)
            {
                savingAccount.AccountHolderName = name;
            }
            else if (_account is CurrentAccount currentAccount)
            {
                currentAccount.AccountHolderName = name;
            }
            return this;
        }

        public IBankAccount Build()
        {
            return _account;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternApp
{
    public class BankManager
    {
        private static BankManager _instance;
        private static readonly object _lock = new object();

        private BankManager() { }

        public static BankManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BankManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public void OpenAccount(IBankAccount account)
        {
            account.DisplayDetails();
        }
    }

}

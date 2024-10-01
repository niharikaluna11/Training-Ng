using System.Runtime.Serialization;

namespace BankApplication
{
    
    internal class InsufficientBalanceException : Exception
    {
        string msg;


        public InsufficientBalanceException(string message)
        {
            msg = message;
        }

        public override string Message => msg;

    }
}
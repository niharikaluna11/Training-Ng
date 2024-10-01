using System.Runtime.Serialization;

namespace BankApplicationNew
{
    internal class CuurentAccountLimitException : Exception
    {
        string msg;


        public CuurentAccountLimitException(string message)
        {
            msg = message;
        }

        public override string Message => msg;


    }
}
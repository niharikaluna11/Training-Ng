using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    public class CouldNotUpdateException : Exception
    {
        string msg;

        public CouldNotUpdateException(string msg)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
}
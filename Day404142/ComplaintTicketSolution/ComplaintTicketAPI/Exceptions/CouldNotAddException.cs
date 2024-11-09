using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    public class CouldNotAddException : Exception
    {
        string msg;

        public CouldNotAddException(string? message) : base(message)
        {
        }

        public CouldNotAddException(string msg, Exception ex)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
}
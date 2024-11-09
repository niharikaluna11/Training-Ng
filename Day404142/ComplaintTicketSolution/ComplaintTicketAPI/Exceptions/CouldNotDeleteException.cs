using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    public class CouldNotDeleteException : Exception
    {
        string msg;

        public CouldNotDeleteException(string msg)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
    
}
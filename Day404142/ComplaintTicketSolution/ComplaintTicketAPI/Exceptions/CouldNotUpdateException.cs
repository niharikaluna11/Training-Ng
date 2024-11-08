using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    public class CouldNotUpdateException : Exception
    {
        public CouldNotUpdateException()
        {
        }

        public CouldNotUpdateException(string? message) : base(message)
        {
        }

        public CouldNotUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CouldNotUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
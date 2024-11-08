using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    public class CouldNotAddException : Exception
    {
        public CouldNotAddException()
        {
        }

        public CouldNotAddException(string? message) : base(message)
        {
        }

        public CouldNotAddException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CouldNotAddException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
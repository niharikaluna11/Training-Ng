using System.Runtime.Serialization;

namespace ComplaintTicketAPI.Exceptions
{
    [Serializable]
    internal class CouldNotDeleteException : Exception
    {
        public CouldNotDeleteException()
        {
        }

        public CouldNotDeleteException(string? message) : base(message)
        {
        }

        public CouldNotDeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CouldNotDeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
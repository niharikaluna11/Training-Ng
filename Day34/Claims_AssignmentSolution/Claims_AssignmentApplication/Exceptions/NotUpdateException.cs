using System.Runtime.Serialization;

namespace Claims_AssignmentApplication.Exceptions
{
    [Serializable]
    internal class NotUpdateException : Exception
    {
        public NotUpdateException()
        {
        }

        public NotUpdateException(string? message) : base(message)
        {
        }

        public NotUpdateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Runtime.Serialization;

namespace EFFirstAPI.Exceptions
{
    [Serializable]
    public class NotUpdateException : Exception
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
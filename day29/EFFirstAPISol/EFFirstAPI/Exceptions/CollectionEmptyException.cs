using System.Runtime.Serialization;

namespace EFFirstAPI.Exceptions
{
    [Serializable]
    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException()
        {
        }

        public CollectionEmptyException(string? message) : base(message)
        {
        }

        public CollectionEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CollectionEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System.Runtime.Serialization;

namespace TrialApplication.Exceptions
{
    [Serializable]
    internal class CollectionEmptyException : Exception
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
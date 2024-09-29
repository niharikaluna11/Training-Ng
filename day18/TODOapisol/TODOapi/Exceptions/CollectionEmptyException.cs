namespace TODOapi.Exceptions
{
    public class CollectionEmptyException : Exception
    {
        String message;
        public CollectionEmptyException()
        {
            message = "Collection is empty";
        }

        public CollectionEmptyException(string? entity)
        {
            message = $"Collection of {entity} is empty";
        }
        public override string Message => message;
    }
}

namespace ReportClaimApplication.Exceptions
{
    public class CollectionEmptyException : Exception
    {
        public CollectionEmptyException(string entityName)
            : base($"{entityName} collection is empty.")
        {
        }
    }
}

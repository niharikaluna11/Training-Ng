namespace ReportClaimApplication.Exceptions
{
    public class CouldNotAddException : Exception
    {
        public CouldNotAddException(string entityName)
            : base($"Could not add {entityName}.")
        {
        }
    }
}

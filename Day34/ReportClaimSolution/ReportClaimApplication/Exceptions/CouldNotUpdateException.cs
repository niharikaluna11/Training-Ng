namespace ReportClaimApplication.Exceptions
{
    public class CouldNotUpdateException : Exception
    {
        public CouldNotUpdateException(string entityName)
            : base($"Could not update {entityName}.")
        {
        }
    }
}

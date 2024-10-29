namespace ReportClaimApplication.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName)
            : base($"{entityName} not found.")
        {
        }
    }
}

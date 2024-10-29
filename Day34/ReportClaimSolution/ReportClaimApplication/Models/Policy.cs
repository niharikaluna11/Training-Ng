namespace ReportClaimApplication.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }          // Unique identifier for the policy
        public string PolicyNumber { get; set; }   // Policy number associated with the insured
        public string InsuredName { get; set; }    // Name of the person insured under the policy
        public string PolicyStatus { get; set; }   // Active, Lapsed, Cancelled, etc.
    }

}

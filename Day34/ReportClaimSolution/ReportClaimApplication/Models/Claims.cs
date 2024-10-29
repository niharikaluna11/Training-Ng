namespace ReportClaimApplication.Models
{
    public class Claims
    {
        public int ClaimId { get; set; }              // Unique identifier for the claim
        
        public Policy Policy { get; set; }             // navigation property of policy 1 to 1 relationship
        public int PolicyId { get; set; }      // Policy number associated with the claim
        public string ClaimType { get; set; }         // Type of claim (e.g., Death Claim, Disability Claim)
        public DateTime DateOfIncident { get; set; }  // Date when the incident occurred
        public string ClaimantName { get; set; }      // Name of the person filing the claim
        public string ClaimantPhone { get; set; }     // Phone number of the claimant
        public string ClaimantEmail { get; set; }     // Email of the claimant
        public string ClaimStatus { get; set; }       // Status of the claim (e.g., Pending, Approved, Rejected)
        public List<Document> Documents { get; set; } // List of uploaded documents related to the claim
    }

}

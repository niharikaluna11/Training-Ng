using System.Reflection.Metadata;

namespace Claims_AssignmentApplication.Models
{
    public enum ClaimType
    {
        DeathClaim,          // For death claims
        DisabilityClaim,     // For disability claims
        AccidentClaim,       // For accident claims
        CriticalIllnessClaim // For critical illness claims
                             // You can add more types as needed
    }

    public class Claims
    {
       
        public int ClaimId { get; set; }              // Unique identifier for the clai
        public string PolicyNumber { get; set; }      // Policy number associated with the claim

        public ClaimType TypeOfClaim { get; set; }
          
        // Type of claim (e.g., Death Claim, Disability Claim)
        public DateTime DateOfIncident { get; set; }  // Date when the incident occurred
        public string ClaimantName { get; set; }      // Name of the person filing the claim
        public string ClaimantPhone { get; set; }     // Phone number of the claimant
        public string ClaimantEmail { get; set; }     // Email of the claimant
        public string ClaimStatus { get; set; }  // Status of the claim (e.g., Pending, Approved, Rejected)
        public List<Document> Documents { get; set; } // List of uploaded documents related to the claim
    }
}

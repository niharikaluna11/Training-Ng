namespace Claims_AssignmentApplication.Models.DTOs
{
    public class ClaimResponseDTO
    {
        public int ClaimId { get; set; }              // Unique identifier for the claim
        public string PolicyNumber { get; set; }      // Policy number associated with the claim
        public ClaimType ClaimType { get; set; }         // Type of claim (e.g., Death Claim, Disability Claim)
        public DateTime DateOfIncident { get; set; }  // Date of the incident
        public string ClaimantName { get; set; }      // Name of the claimant
        public string ClaimantPhone { get; set; }     // Claimant's phone number
        public string ClaimantEmail { get; set; }     // Claimant's email
        public string ClaimStatus { get; set; }       // Current status of the claim (e.g., Pending, Approved, Rejected)
        public List<string> DocumentUrls { get; set; } // List of URLs of uploaded documents related to the claim

    }
}

namespace ReportClaimApplication.Models.DTO
{
    public class ClaimRequestDTO
    {
        public string PolicyNumber { get; set; }      // Selected policy number for the claim
        public string ClaimType { get; set; }         // Type of claim (e.g., Death Claim, Disability Claim)
        public DateTime DateOfIncident { get; set; }  // Date when the incident occurred
        public string ClaimantName { get; set; }      // Name of the claimant
        public string ClaimantPhone { get; set; }     // Claimant's phone number
        public string ClaimantEmail { get; set; }     // Claimant's email address

        // Add the Documents property
        public List<DocumentUploadDTO> Documents { get; set; } = new List<DocumentUploadDTO>();
    }

}

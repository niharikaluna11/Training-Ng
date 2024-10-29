namespace Claims_AssignmentApplication.Models.DTOs
{
    public class ClaimRequestDTO
    {
        public string PolicyNumber { get; set; }  // Assuming this is selected from a dropdown or search
        public ClaimType ClaimType { get; set; }  // Enum for claim type
        public DateTime DateOfIncident { get; set; }
        public string ClaimantName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool TermsAccepted { get; set; }
    }
}

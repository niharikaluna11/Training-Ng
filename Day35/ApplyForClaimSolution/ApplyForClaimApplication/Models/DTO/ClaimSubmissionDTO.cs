namespace ApplyForClaimApplication.Models.DTO
{
    public class ClaimSubmissionDTO
    {
        public PolicyDTO Policy { get; set; }

        public ClaimTypeDTO ClaimType { get; set; }
        public ClaimRequestDTO ClaimRequestDTO { get; set; }

        public DocumentDTO Document { get; set; }
        public bool TermsAccepted { get; set; }
    }
}

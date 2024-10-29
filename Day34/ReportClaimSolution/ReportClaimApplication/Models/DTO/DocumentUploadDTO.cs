namespace ReportClaimApplication.Models.DTO
{
    public class DocumentUploadDTO
    {
        public int ClaimId { get; set; }              // Claim ID to associate the document with a specific claim
        public string DocumentName { get; set; }      // Name of the uploaded document
        public string DocumentUrl { get; set; }   
    }

}

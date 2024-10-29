namespace ReportClaimApplication.Models
{
    public class Document
    {
        public int DocumentId { get; set; }        // Unique identifier for the document
        public int ClaimId { get; set; }           // Foreign key to associate the document with a claim
        public string DocumentName { get; set; }   // Name of the document file
        public string DocumentUrl { get; set; }    // URL or path to the document location
        public DateTime UploadedDate { get; set; } // Date when the document was uploaded

        public Claims claim { get; set; }
    }

}

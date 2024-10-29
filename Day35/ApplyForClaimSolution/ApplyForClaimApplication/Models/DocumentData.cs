using System.Security.Claims;

namespace ApplyForClaimApplication.Models
{
    public class DocumentData
    {
        public int DocumentId { get; set; }        // Unique identifier for the document
        public int ClaimId { get; set; }           // Foreign key to associate the document with a claim
        public byte[] Settlementform  { get; set; }   // Name of the document file

        public byte[] DeathCertificate { get; set; }   // Name of the document file
        public byte[] PolicyCertificate { get; set; }   // Name of the document file
        public byte[] Photo { get; set; }   // Name of the document file
        public byte[] AddressProof { get; set; }   // Name of the document file
        public byte[] CancelledCheck { get; set; }   // Name of the document file

        public byte[] Others { get; set; }   // Name of the document file
        public string DocumentUrl { get; set; }    // URL or path to the document location
        public DateTime UploadedDate { get; set; } // Date when the document was uploaded

        public ClaimData claim { get; set; }
    }
}

namespace ApplyForClaimApplication.Models.DTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }        // Unique identifier for the document
        public byte[] Settlementform { get; set; }   // Name of the document file

        public byte[] DeathCertificate { get; set; }   // Name of the document file
        public byte[] PolicyCertificate { get; set; }   // Name of the document file
        public byte[] Photo { get; set; }   // Name of the document file
        public byte[] AddressProof { get; set; }   // Name of the document file
        public byte[] CancelledCheck { get; set; }   // Name of the document file

        public byte[] Others { get; set; }   // Name of the document file
        public string DocumentUrl { get; set; }    // URL or path to the document location
       
    }
}

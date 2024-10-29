namespace Claims_AssignmentApplication.Models
{
    public enum DocumentName
    {
        DeathCertificate,          
        PolicyCertificate,   
        PhotoID,       
        AddressProof,
        CancelledCheck,
        Others
    }

    public class Document
    {
        public int DocumentId { get; set; }       
        public int ClaimId { get; set; }           
        public DocumentName DocumentName { get; set; }   
        public string DocumentUrl { get; set; }   
        public DateTime UploadedDate { get; set; } 
        public Claims claim { get; set; }
    }

}

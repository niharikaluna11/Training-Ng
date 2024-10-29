namespace Claims_AssignmentApplication.Models.DTOs
{
    public class DocumentUploadDTO
    {

        public int ClaimId { get; set; }
        public DocumentName DocumentName { get; set; }  // Enum for document name
        public string DocumentUrl { get; set; }  // URL or path to the document
        public DateTime UploadedDate { get; set; } = DateTime.Now;
    }
}

namespace ComplaintTicketAPI.Models.DTO
{
    public class UpdateComplaintResponseDTO
    {
        public int ComplaintId { get; set; }
        public Status Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Message { get; set; }
    }

}

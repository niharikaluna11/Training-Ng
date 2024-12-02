namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintDetailsDto
    {
        public int ComplaintId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Category { get; set; }
        public string LatestStatus { get; set; }
        public string Priority { get; set; }
    }
}

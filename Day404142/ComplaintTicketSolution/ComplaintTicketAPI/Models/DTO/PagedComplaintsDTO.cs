namespace ComplaintTicketAPI.Models.DTO
{
    public class PagedComplaintsDTO
    {
        public List<ComplaintDataDTO> Complaints { get; set; }
        public int TotalCount { get; set; }
    }
}

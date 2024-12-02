namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintDataDTO
    {
        public int complaintId { get; set; }
        public string Description { get; set; }

        public string status { get; set; }  
        public string Priority { get; set; }
    }
}

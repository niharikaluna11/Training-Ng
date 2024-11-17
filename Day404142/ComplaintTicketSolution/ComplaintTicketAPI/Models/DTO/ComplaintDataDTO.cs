namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintDataDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string CategoryName { get; set; } // To display category name instead of the whole object
        public DateTime LastUpdated { get; set; } // Example: to show last status update time
    }
}

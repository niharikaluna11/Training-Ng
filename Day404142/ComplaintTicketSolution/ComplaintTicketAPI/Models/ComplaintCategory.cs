namespace ComplaintTicketAPI.Models
{
    public class ComplaintCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } // Category name
        public string Description { get; set; } // Category description

        public ICollection<Complaint> Complaints { get; set; }
    }
}

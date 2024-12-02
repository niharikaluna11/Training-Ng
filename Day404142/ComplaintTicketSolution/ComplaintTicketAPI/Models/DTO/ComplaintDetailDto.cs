namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintDetailDto
    {
        public int ComplaintId { get; set; }
        public string CategoryName { get; set; }
        public List<string> Files { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? LatestStatusDate { get; set; }

        // User details
        public User UserDetails { get; set; }

        // Organization details
        public Organization OrganizationDetails { get; set; }
    }
}

namespace ComplaintTicketAPI.Models.DTO
{
    public class OrganizationProfileDTO
    {
        public string Name { get; set; }
        public Type Types { get; set; }
        // Enum or string: Company 1, Government 2, Agent 3
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}


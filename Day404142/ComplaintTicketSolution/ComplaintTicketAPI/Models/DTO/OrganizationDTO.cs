namespace ComplaintTicketAPI.Models.DTO
{
    public class OrganizationDTO
    {
        public int orgId { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public Type Types { get; set; }

        public String? ProfilePicture { get; set; }
      
      
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

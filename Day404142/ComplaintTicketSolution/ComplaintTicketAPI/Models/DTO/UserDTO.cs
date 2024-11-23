namespace ComplaintTicketAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public PersonStatus PStatus { get; set; }
        public string Username { get; set; }
        public Role Roles { get; set; }

    }
}

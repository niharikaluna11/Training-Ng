namespace ComplaintTicketAPI.Models.DTO
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string Email { get; set; }

        public PersonStatus PStatus { get; set; }
        public string Username { get; set; }
        public Role Roles { get; set; }

    }
}

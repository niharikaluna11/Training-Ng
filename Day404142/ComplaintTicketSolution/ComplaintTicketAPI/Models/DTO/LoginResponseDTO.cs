namespace ComplaintTicketAPI.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }

        public string Role {  get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
        public int Id { get; internal set; }
    }
}

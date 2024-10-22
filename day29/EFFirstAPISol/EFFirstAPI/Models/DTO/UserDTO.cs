namespace EFFirstAPI.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }=string.Empty;

        public string Password { get; set; } = string.Empty;

        public Roles Role {  get; set; }
    
    }
}

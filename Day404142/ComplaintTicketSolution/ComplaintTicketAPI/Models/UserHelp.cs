using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models
{
    public class UserHelp
    {
        [Key]
        public int Id { get; set; }

        public string email { get; set; }

        public string query { get; set; }
        public bool IsResponded { get; set; } = false;
        public string? AdminResponse { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class EmailDTO
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
    }
}

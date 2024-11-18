using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class CreateComplaintRequestDTO
    {
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }            // ID of the user creating the complaint

        [Required(ErrorMessage = "Organization is required")]
        public int OrganizationId { get; set; }     // ID of the organization related to the complaint

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }         // ID for the complaint category (e.g., Technical, Customer Service)

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }     // Detailed description of the complaint

        [Required(ErrorMessage = "Priority is required")]
        public Priority Priority { get; set; }      // Priority level: Low, Medium, High, Urgent
        
        public Status status {  get; set; } 

        public string CommentByUser { get; set; }   // Comment by user when submitting the complaint

        [Required(ErrorMessage = "Atleast One File is required")]
        public List<IFormFile> AttachmentUrl { get; set; }

        [Required(ErrorMessage = "Please accept the terms and conditions")]
        public bool AcceptTermsAndConditions { get; set; }
    }
}
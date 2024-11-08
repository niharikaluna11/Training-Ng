using System.ComponentModel.DataAnnotations;

namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintCategoryDTO
    {
        [Required(ErrorMessage = " Category Name is required")]
        public string Name { get; set; } // Category name
        public string Description { get; set; } // Category description
    }
}

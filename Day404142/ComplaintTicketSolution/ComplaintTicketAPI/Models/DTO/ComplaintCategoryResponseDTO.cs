namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintCategoryResponseDTO
    {
        public int categoryId { get; set; }                 // Unique identifier
        public string Name { get; set; }            // Category name
        public string Description { get; set; }     // Category description
    }
}

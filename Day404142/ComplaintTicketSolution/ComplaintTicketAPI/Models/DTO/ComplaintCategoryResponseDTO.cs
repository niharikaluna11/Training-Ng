namespace ComplaintTicketAPI.Models.DTO
{
    public class ComplaintCategoryResponseDTO
    {
        public int Id { get; set; }                 // Unique identifier
        public string Name { get; set; }            // Category name
        public string Description { get; set; }     // Category description
    }
}

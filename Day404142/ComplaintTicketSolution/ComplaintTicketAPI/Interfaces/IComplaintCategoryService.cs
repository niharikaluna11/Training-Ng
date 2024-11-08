using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IComplaintCategoryService
    {
        Task<IEnumerable<ComplaintCategoryResponseDTO>> GetAllComplaintCategories();
        Task<ComplaintCategoryResponseDTO> AddComplaintCategory(ComplaintCategoryDTO category);
    }
}

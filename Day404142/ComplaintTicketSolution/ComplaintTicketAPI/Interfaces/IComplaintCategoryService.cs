using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface IComplaintCategoryService
    {
        Task<IEnumerable<ComplaintCategoryResponseDTO>> GetAllComplaintCategories(int pagenum,int pagesize);
        Task<ComplaintCategoryResponseDTO> AddComplaintCategory(ComplaintCategoryDTO category);

        
    }
}

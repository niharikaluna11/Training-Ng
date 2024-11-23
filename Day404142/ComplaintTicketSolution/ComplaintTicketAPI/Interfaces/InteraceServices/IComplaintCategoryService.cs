using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces.InteraceServices
{
    public interface IComplaintCategoryService
    {
        Task<IEnumerable<ComplaintCategoryResponseDTO>> GetAllComplaintCategories(int pagenum, int pagesize);
        Task<ComplaintCategoryResponseDTO> AddComplaintCategory(ComplaintCategoryDTO category);


    }
}

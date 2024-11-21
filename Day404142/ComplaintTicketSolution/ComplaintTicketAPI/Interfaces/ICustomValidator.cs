using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface ICustomValidator
    {
        Task<BaseResponseDTO> ValidateAsync(object value);
    }
}

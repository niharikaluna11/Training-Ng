using ComplaintTicketAPI.Models.DTO.ResponseDTO;

namespace ComplaintTicketAPI.Interfaces
{
    public interface ICustomValidator
    {
        Task<BaseResponseDTO> ValidateAsync(object value);
    }
}

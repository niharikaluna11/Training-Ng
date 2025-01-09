using BankManagementApplication.Models.DTOs;

namespace BankManagementApplication.Interfaces
{
    public interface ICustomValidator
    {
        Task<BaseResponseDTO> ValidateAsync(object value);
    }
}

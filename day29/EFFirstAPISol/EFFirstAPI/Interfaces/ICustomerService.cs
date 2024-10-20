using EFFirstAPI.Models.DTO;

namespace EFFirstAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(CustomerDTO customer);
    }
}

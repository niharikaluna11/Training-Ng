using BankManagementApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApplication.Interfaces.Repositories
{
    public interface ICustomerRepository  : IRepository<int, Customer>
    {
        Task<IEnumerable<Customer>> GetByAccountType(string accountType);

        Task<IEnumerable<Customer>> GetByFName(string firstName);
        Task<IEnumerable<Customer>> GetByLName(string lastName);

        Task<IEnumerable<Customer>> GetCustomersByAccountNumber(string accountNumber);

        Task<IEnumerable<Customer>> GetCustomersByPhoneNumber(string phoneNumber);
      

    }
}

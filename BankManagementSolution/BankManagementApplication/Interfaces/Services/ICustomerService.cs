using BankManagementApplication.Models;
using BankManagementApplication.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankManagementApplication.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<BaseResponseDTO> CreateCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<BaseResponseDTO> UpdateCustomer(int customerId, Customer customer);
        Task<BaseResponseDTO> DeleteCustomer(int customerId);
        Task<BaseResponseDTO> GetCustomerById(int customerId);

        //Search customer by firstname, lastname, account number, phone number 

        Task<IEnumerable<Customer>> GetCustomersByFName(string firstName);
        Task<IEnumerable<Customer>> GetCustomersByLName(string firstName);

        Task<IEnumerable<Customer>> GetCustomersByAccountNumber(string accountNumber);


        Task<IEnumerable<Customer>> GetCustomersByPhoneNumber(string phoneNumber);
       


    }
}

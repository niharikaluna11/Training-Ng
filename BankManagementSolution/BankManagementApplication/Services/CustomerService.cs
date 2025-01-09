using AutoMapper;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Interfaces.Services;
using BankManagementApplication.Models;
using BankManagementApplication.Models.DTOs;
using BankManagementApplication.Repositories;
using BankManagementApplication.Validations;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace BankManagementApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
              ILogger<CustomerService> logger,
              IMapper mapper,
              ICustomerRepository customerRepository
        )
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponseDTO> CreateCustomer(Customer customer)
        {
            try
            {
                var emailValidator = new EmailValidator(_customerRepository);
                var emailValidationResult = await emailValidator.ValidateAsync(customer.Email);

                if (!emailValidationResult.Success)
                {
                    return emailValidationResult;
                }

                var numValidator = new PhoneNumberValidator(_customerRepository);
                var numValidationResult = await numValidator.ValidateAsync(customer.PhoneNumber);

                if (!numValidationResult.Success)
                {
                    return numValidationResult;
                }

                _logger.LogInformation("Creating new customer.");

                var customerEntity = _mapper.Map<Customer>(customer); 
                var createdCustomer = await _customerRepository.Add(customerEntity);

                _logger.LogInformation($"Customer with ID: {createdCustomer.CustomerId} created successfully.");
                return new SuccessResponseDTO<Customer>
                {
                    Message = "Customer created successfully",
                    Data = createdCustomer,
                    Success = true
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating the customer: {ex.Message}");
                return new ErrorResponseDTO
                {
                    ErrorMessage = "Error occurred while deleting the customer. ",
                    Success = false,
                    ErrorCode = 0

                };
            }
        }

        public async Task<BaseResponseDTO> DeleteCustomer(int customerId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete customer with ID: {customerId}");

                var customer = await _customerRepository.Get(customerId);
                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID: {customerId} not found.");
                    return new ErrorResponseDTO
                    {
                        ErrorMessage= "Customer not found. ",
                        Success= false,
                        ErrorCode= 404

                    };
                }

                var deletedCustomer = await _customerRepository.Delete(customer, customerId);
                _logger.LogInformation($"Customer with ID: {deletedCustomer.CustomerId} deleted successfully.");
                return new BaseResponseDTO
                {
                    Success = true,
                    Message=" Customer Deleted Succesfully "
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting the customer: {ex.Message}");
                return new ErrorResponseDTO
                {
                    ErrorMessage = "Error occurred while deleting the customer. ",
                    Success = false,
                    ErrorCode = 0

                };
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                _logger.LogInformation("Fetching all customers.");
                var customers = await _customerRepository.GetAll();
                return customers;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                throw;
            }
        }

      
        public async Task<BaseResponseDTO> GetCustomerById(int customerId)
        {
            try
            {
                _logger.LogInformation($"Fetching customer with ID: {customerId}");
                var customer = await _customerRepository.Get(customerId);
                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID: {customerId} not found.");
                    
                    return new ErrorResponseDTO
                    {
                        Success = false,
                        ErrorMessage= $"Customer with ID: {customerId} not found.",
                        ErrorCode = 404
                    };
                }
                return new SuccessResponseDTO<Customer>
                {
                    Data = customer,
                    Success = true,
                    Message=$" The customer with ID:{customerId} details are shown "
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving the customer: {ex.Message}");
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = $"Exception occured",
                    ErrorCode = 504
                };
            }
        }

        public async Task<BaseResponseDTO> UpdateCustomer(int customerId, Customer customer)
        {
            try
            {

                var emailValidator = new EmailValidator(_customerRepository);
                var emailValidationResult = await emailValidator.ValidateAsync(customer.Email);

                if (!emailValidationResult.Success)
                {
                    return emailValidationResult;
                }

                var numValidator = new PhoneNumberValidator(_customerRepository);
                var numValidationResult = await numValidator.ValidateAsync(customer.PhoneNumber);

                if (!numValidationResult.Success)
                {
                    return numValidationResult;
                }
                _logger.LogInformation($"Updating customer with ID: {customerId}");

                var existingCustomer = await _customerRepository.Get(customerId);
                if (existingCustomer == null)
                {
                    return new ErrorResponseDTO
                    {
                        ErrorMessage= $"Customer with ID {customerId} not found.",
                        ErrorCode = 504

                    };
                }
                if(existingCustomer.CStatus==CustomerStatus.Deactivated)
                {
                    return new ErrorResponseDTO
                    {
                        ErrorMessage = $"Customer with ID {customerId} is already deleted.",
                        ErrorCode = 504

                    };
                }

                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                existingCustomer.PhoneNumber = customer.PhoneNumber;
                existingCustomer.AccountType = customer.AccountType;
                existingCustomer.AccountNumber = customer.AccountNumber;


                var updatedCustomer = await _customerRepository.Update(existingCustomer,customerId);

                _logger.LogInformation($"Customer with ID: {updatedCustomer.CustomerId} updated successfully.");
                return new SuccessResponseDTO<Customer>
                {
                    Success = true,
                    Message ="customer updated succesfully",
                    Data = updatedCustomer
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating the customer: {ex.Message}");
                return new ErrorResponseDTO
                {
                    Success = false,
                    ErrorMessage = $"Exception occured",
                    ErrorCode = 504
                };
            }
        }

        async Task<IEnumerable<Customer>> ICustomerService.GetCustomersByAccountNumber(string accountNumber)
        {
            try
            {
                var customers = await _customerRepository.GetCustomersByAccountNumber(accountNumber);

                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                throw;
            }
        }

        async Task<IEnumerable<Customer>> ICustomerService.GetCustomersByFName(string firstName)
        {
            try
            {
                var customers = await _customerRepository.GetByFName(firstName);

                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                throw;
            }
         
        }

        async Task<IEnumerable<Customer>> ICustomerService.GetCustomersByLName(string firstName)
        { 
          try
             {
               var customers = await _customerRepository.GetByLName(firstName);

                return customers;
             }
          catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                throw;
            }
        }

        async Task<IEnumerable<Customer>> ICustomerService.GetCustomersByPhoneNumber(string phoneNumber)
        {
            try
            {
                var customers = await _customerRepository.GetCustomersByPhoneNumber(phoneNumber);

                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                throw;
            }
        }
    }
}

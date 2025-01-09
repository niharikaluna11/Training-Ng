using AutoMapper;
using BankManagementApplication.Interfaces.Services;
using BankManagementApplication.Models;
using BankManagementApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankManagementApplication.Controllers
{
    [Route("api/[controller]")]


    [ApiController]
    [EnableCors("AllowAll")]

    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
      
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerService customerService,
             
            ILogger<CustomerController> logger,
            IMapper mapper)
        {
            _customerService = customerService;
            
            _logger = logger;
            _mapper = mapper;
        }

        //get all customerss
        [HttpGet("GetAllCustomer")]
        [Authorize(Roles = "ZoneManager,Teller,BranchManager")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
             

                var allCustomers = await _customerService.GetAllCustomers();
                if (allCustomers == null)
                {
                    return StatusCode(204, "no customer found");
                }
                return Ok(allCustomers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving customers: {ex.Message}");
                return StatusCode(500, "An error occurred while getting all the customer.");
            }
        }

        //get customer by its id
        [HttpGet("GetCustomerByID")]
        [Authorize(Roles = "ZoneManager,BranchManager,Teller")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {

            var response = await _customerService.GetCustomerById(customerId);

            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            return Ok(response);
        }



        //create a customerr
        [HttpPost("CreateCustomer")]
        [Authorize(Roles = "ZoneManager,BranchManager")]
        public async Task<IActionResult> CreateCustomer([FromForm] CustomerRequestDTO customer)
        {
            
            try
            {
                var customerEntity = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,
                    AccountType = customer.AccountType,
                    AccountNumber = customer.AccountNumber
                };

                var response = await _customerService.CreateCustomer(customerEntity);

                _logger.LogInformation($"Response Success: {response.Success}");
               
                    return Ok(response);
                
               

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating the customer: {ex.Message}");
                return StatusCode(500, "An error occurred while creating customer.");
            }
        }

        //deactivate a customer or lets say delete
        [HttpDelete("DeleteCustomer")]
        [Authorize(Roles = "ZoneManager,BranchManager")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid customer ID.");
            }

            try
            {
                var response = await _customerService.DeleteCustomer(id);

                if (response.Success)
                {
                    return Ok(response);
                }

                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting the customer: {ex.Message}");
                return StatusCode(500, "An error occurred while deleeting customer.");
            }
        }

        

       
       //update customer info
        [HttpPut("UpdateCustomer")]
        [Authorize(Roles = "ZoneManager,BranchManager")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromForm] CustomerRequestDTO customer)
        {
            try
            {
                
                var customerEntity = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    PhoneNumber = customer.PhoneNumber,

                    AccountType = customer.AccountType,
                    AccountNumber = customer.AccountNumber
                };
                var updatedCustomer = await _customerService.UpdateCustomer(id, customerEntity);
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating the customer: {ex.Message}");
                return StatusCode(500, "An error occurred while updatingg customer.");
            }
        }

        //searching thing

        //getbyFIRSTname
        [HttpGet("GetBYfirstName")]
        [Authorize(Roles = "ZoneManager,Teller,BranchManager")]
        public async Task<IActionResult> GetCustomersByFName(string firstName)
        {
            var customers = await _customerService.GetCustomersByFName(firstName);
            if (customers == null || !customers.Any())
            {
                return NotFound($"No customers found with first name: {firstName}");
            }
            return Ok(customers);
        }

        //getbyLASTname
        [HttpGet("getBYlastName")]
        [Authorize(Roles = "ZoneManager,Teller,BranchManager")]
        public async Task<IActionResult> GetCustomersByLName(string lastName)
        {
            var customers = await _customerService.GetCustomersByLName(lastName);
            if (customers == null || !customers.Any())
            {
                return NotFound($"No customers found with last name: {lastName}");
            }
            return Ok(customers);
        }


       
        [HttpGet("getBYaccountNumber")]
        [Authorize(Roles = "ZoneManager,Teller,BranchManager")]
        public async Task<IActionResult> GetCustomersByAccountNumber(string accountNumber)
        {
            var customers = await _customerService.GetCustomersByAccountNumber(accountNumber);
            if (customers == null || !customers.Any())
            {
                return NotFound($"No customers found with account number: {accountNumber}");
            }
            return Ok(customers);
        }

        
        [HttpGet("getBYphoneNumber")]
        [Authorize(Roles = "ZoneManager,Teller,BranchManager")]
        public async Task<IActionResult> GetCustomersByPhoneNumber(string phoneNumber)
        {
            var customers = await _customerService.GetCustomersByPhoneNumber(phoneNumber);
            if (customers == null || !customers.Any())
            {
                return NotFound($"No customers found with phone number: {phoneNumber}");
            }
            return Ok(customers);
        }


    }
}

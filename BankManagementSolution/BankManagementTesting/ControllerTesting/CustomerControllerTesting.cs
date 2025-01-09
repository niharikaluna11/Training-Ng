using AutoMapper;
using BankManagementApplication.Controllers;
using BankManagementApplication.Interfaces.Services;
using BankManagementApplication.Models.DTOs;
using BankManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementTesting.ControllerTesting
{
    public class CustomerControllerTesting
    {
        private Mock<ICustomerService> _mockCustomerService;
        private Mock<ILogger<CustomerController>> _mockLogger;
        private CustomerController _customerController;

        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _mockLogger = new Mock<ILogger<CustomerController>>();
            _mockMapper = new Mock<IMapper>();
            _customerController = new CustomerController(_mockCustomerService.Object, _mockLogger.Object, _mockMapper.Object);  // Pass the mock to the constructor
        }


        [Test]
        public async Task CreateCustomer_ShouldReturnOk_WhenCustomerIsCreatedSuccessfully()
        {
            // Arrange
            var customerDto = new CustomerRequestDTO
            {
                FirstName = "nihaa",
                LastName = "garggg",
                Email = "nihaa@gmail.com",
                Address = "114 hardev",
                PhoneNumber = "9810447565",
                AccountType = AccountType.Checking,
                AccountNumber = "acc11"
            };

            var customerEntity = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Address = customerDto.Address,
                PhoneNumber = customerDto.PhoneNumber,
                AccountType = customerDto.AccountType,
                AccountNumber = customerDto.AccountNumber
            };

            var response = new BaseResponseDTO { Success = true, Message = "Customer created successfully" };

            _mockCustomerService.Setup(service => service.CreateCustomer(It.IsAny<Customer>()))
                                .ReturnsAsync(response);

      
            var result = await _customerController.CreateCustomer(customerDto);

          
            var okResult = result as OkObjectResult; 
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode); 
            Assert.AreEqual(response, okResult.Value); 
        }

        [Test]
        public async Task CreateCustomer_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            
            var customerRequestDTO = new CustomerRequestDTO
            {
                FirstName = "nihaa",
                LastName = "patel",
                Email = "patel.niharika@gmail.com",
                PhoneNumber = "1234567890",
                Address = "delhi chandni chowk",
                AccountType = AccountType.Checking,
                AccountNumber = "acc12345"
            };

            _mockCustomerService.Setup(service => service.CreateCustomer(It.IsAny<Customer>()))
                                .ThrowsAsync(new Exception("Error during customer creation"));

            
            var result = await _customerController.CreateCustomer(customerRequestDTO);

            
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("An error occurred while processing your request.", objectResult.Value);
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnOk_WhenCustomersExist()
        {
          
            var customers = new[]
            {
                new Customer { CustomerId = 1, FirstName = "niha", LastName = "garg" },
                new Customer { CustomerId = 2, FirstName = "disha", LastName = "garg" }
            };

            _mockCustomerService.Setup(service => service.GetAllCustomers()).ReturnsAsync(customers);

          
            var result = await _customerController.GetAllCustomers();

          
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var response = okResult.Value as Customer[];
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Length);
        }

        
        [Test]
        public async Task UpdateCustomer_ShouldReturnOk_WhenCustomerIsUpdatedSuccessfully()
        {
            var customerId = 1;
            var customerRequestDTO = new CustomerRequestDTO
            {
                FirstName = "saurabh",
                LastName = "guptaa",
                Email = "gupta.saurabh@example.com",
                PhoneNumber = "98128739012",
                Address = "chennai vellore",
                AccountType = AccountType.Checking,
                AccountNumber = "67890"
            };

            var updatedCustomer = new Customer
            {
                CustomerId = customerId,
                FirstName = customerRequestDTO.FirstName,
                LastName = customerRequestDTO.LastName,
                Email = customerRequestDTO.Email,
                PhoneNumber = customerRequestDTO.PhoneNumber,
                Address = customerRequestDTO.Address,
                AccountType = customerRequestDTO.AccountType,
                AccountNumber = customerRequestDTO.AccountNumber
            };

        
            var result = await _customerController.UpdateCustomer(customerId, customerRequestDTO);

            
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var response = okResult.Value as Customer;
            Assert.IsNotNull(response);
            Assert.AreEqual("saurabh", response.FirstName);
            Assert.AreEqual("guptaa", response.LastName);
        }

       
        [Test]
        public async Task DeleteCustomer_ShouldReturnOk_WhenCustomerIsDeletedSuccessfully()
        {

            var customerId = 1;
            var successResponse = new BaseResponseDTO
            {
                Success = true,
                Message = "Customer deleted successfully"
            };

            _mockCustomerService.Setup(service => service.DeleteCustomer(customerId)).ReturnsAsync(successResponse);

          
            var result = await _customerController.DeleteCustomer(customerId);

          
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var response = okResult.Value as BaseResponseDTO;
            Assert.IsNotNull(response);
            Assert.True(response.Success);
            Assert.AreEqual("Customer deleted successfully", response.Message);
        }

        [Test]
        public async Task DeleteCustomer_ShouldReturnBadRequest_WhenCustomerIdIsInvalid()
        {
            
            var invalidCustomerId = -1;

            
            var result = await _customerController.DeleteCustomer(invalidCustomerId);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid customer ID.", badRequestResult.Value);
        }

    }
}

using AutoMapper;
using BankManagementApplication.Interfaces.Repositories;
using BankManagementApplication.Interfaces.Services;
using BankManagementApplication.Models.DTOs;
using BankManagementApplication.Models;
using BankManagementApplication.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementTesting.ServicesTesting
{
    public class CustomerServiceTesting
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<CustomerService>> _loggerMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private ICustomerService _customerService;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CustomerService>>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();

            _customerService = new CustomerService(
                _loggerMock.Object,
                _mapperMock.Object,
                _customerRepositoryMock.Object
            );
        }


        [Test]
        public async Task CreateCustomer_ValidCustomer_ReturnsSuccessResponse()
        {
           
            var customer = new Customer
            {
                FirstName = "niharika",
                LastName = "garg",
                Email = "ng@gmail.com",
                PhoneNumber = "1234567890",
                AccountNumber = "12345",
                Address = "123 ghaziabad",
                CStatus = CustomerStatus.Activated
            };

            _mapperMock.Setup(m => m.Map<Customer>(It.IsAny<Customer>())).Returns(customer);
            _customerRepositoryMock.Setup(r => r.Add(It.IsAny<Customer>())).ReturnsAsync(customer);

            
            var response = await _customerService.CreateCustomer(customer);

            
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("Customer created succesfully", response.Message);
        }

        [Test]
        public async Task DeleteCustomer_CustomerExists_ReturnsSuccessResponse()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, CStatus = CustomerStatus.Activated };

            _customerRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(customer);
            _customerRepositoryMock.Setup(r => r.Delete(It.IsAny<Customer>(), 1)).ReturnsAsync(customer);

            // Act
            var response = await _customerService.DeleteCustomer(1);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(" Customer Deleted Succesfully ", response.Message);
        }

        [Test]
        public async Task DeleteCustomer_CustomerNotFound_ReturnsErrorResponse()
        {
            // Arrange
            _customerRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Customer)null);

            // Act
            var response = await _customerService.DeleteCustomer(1);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Customer not found. ", ((ErrorResponseDTO)response).ErrorMessage);
        }

        [Test]
        public async Task GetAllCustomers_ReturnsCustomerList()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe" }
            };

            _customerRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllCustomers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }


        [Test]
        public void UpdateCustomer_CustomerDeactivated_ThrowsException()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                CustomerId = 1,
                FirstName = "John",
                LastName = "Doe",
                CStatus = CustomerStatus.Deactivated
            };

            _customerRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(existingCustomer);

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _customerService.UpdateCustomer(1, new Customer()));
            Assert.AreEqual("Customer with ID 1 deactivated.", exception.Message);
        }
    }
}

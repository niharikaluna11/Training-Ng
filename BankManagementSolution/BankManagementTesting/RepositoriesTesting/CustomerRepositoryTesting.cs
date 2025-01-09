using BankManagementApplication.Context;
using BankManagementApplication.Models;
using BankManagementApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementTesting.RepositoriesTesting
{
    public class CustomerRepositoryTesting
    {
        private DbContextOptions<BankManagementContext> _options;
        private BankManagementContext _context;
        private CustomerRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<BankManagementContext>()
                .UseInMemoryDatabase("TestBankDb")
                .Options;

            _context = new BankManagementContext(_options);
            _repository = new CustomerRepository(_context);
        }


        [Test]
        public async Task AddCustomer_ShouldAddCustomerSuccessfully()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 1,
                FirstName = "nikk",
                LastName = "ng",
                AccountNumber ="String11",
                Address="String",
                Email="ng@gmail.com",
                PhoneNumber="9273",
                CStatus = CustomerStatus.Activated
            };

            // Act
            var addedCustomer = await _repository.Add(customer);

            // Assert
            Assert.IsNotNull(addedCustomer);
            Assert.AreEqual(customer.FirstName, addedCustomer.FirstName);
        }

        [Test]
        public async Task GetCustomer_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 2,
                FirstName = "ng",
                LastName = "ng",
                AccountNumber = "String11",
                Address = "String",
                Email = "ngik@gmail.com",
                PhoneNumber = "9273",
                CStatus = CustomerStatus.Activated
            };
            await _repository.Add(customer);

            // Act
            var retrievedCustomer = await _repository.Get(customer.CustomerId);

            // Assert
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual(customer.FirstName, retrievedCustomer.FirstName);
        }

        [Test]
        public async Task GetCustomer_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Act
            var customer = await _repository.Get(987);

            // Assert
            Assert.IsNull(customer);
        }

        [Test]
        public async Task DeleteCustomer_ShouldSoftDeleteCustomerSuccessfully()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = 3,
                
                FirstName = "ng",
                LastName = "ng",
                AccountNumber = "String11",
                Address = "String",
                Email = "ngik@gmail.com",
                PhoneNumber = "9273",
                CStatus = CustomerStatus.Activated
            };
            await _repository.Add(customer);

            // Act
            var deletedCustomer = await _repository.Delete(customer, customer.CustomerId);

            // Assert
            Assert.IsNotNull(deletedCustomer);
            Assert.AreEqual(CustomerStatus.Deactivated, deletedCustomer.CStatus);
        }

        
    }
}

using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Tests.Repositories
{
    public class OrganizationRepositoryTests
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private OrganizationRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new OrganizationRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Add_ShouldAddOrganizationSuccessfully()
        {
            // Arrange
            var organization = new Organization
            {
                UserId = 1,
                Name = "Org1",
                Email = "org1@example.com",
                Phone = "1234567890",
                Address = "123 Org Street",
                Types = (Models.Type)(1)
            };

            // Act
            var result = await _repository.Add(organization);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(organization.UserId, result.UserId);
        }

        [Test]
        public void Add_ShouldThrowCouldNotAddException_WhenAdditionFails()
        {
            // Arrange: Inject invalid data if applicable to cause failure
            var invalidOrganization = new Organization(); // Missing required fields

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(invalidOrganization));
        }

        [Test]
        public async Task Get_ShouldReturnOrganization_WhenExists()
        {
            // Arrange
            var organization = new Organization
            {
                UserId = 2,
                Name = "Org2",
                Email = "org2@example.com",
                Phone = "1234567891",
                Address = "124 Org Avenue",
                Types = (Models.Type)(1)
            };
            await _repository.Add(organization);

            // Act
            var result = await _repository.Get(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(organization.Name, result.Name);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenOrganizationDoesNotExist()
        {
            // Act
            var result = await _repository.Get(99);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllOrganizations()
        {
            // Arrange
            var org1 = new Organization { UserId = 3, Name = "Org3", Email = "org3@example.com", Phone = "1234567892", Address = "125 Org Road", Types =(Models.Type)1 };
            var org2 = new Organization { UserId = 4, Name = "Org4", Email = "org4@example.com", Phone = "1234567893", Address = "126 Org Lane", Types = (Models.Type)1 };
            await _repository.Add(org1);
            await _repository.Add(org2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Update_ShouldUpdateOrganizationSuccessfully()
        {
            // Arrange
            var organization = new Organization
            {
                UserId = 5,
                Name = "Org5",
                Email = "org5@example.com",
                Phone = "1234567894",
                Address = "127 Org Place",
                Types = (Models.Type)(1)
            };
            await _repository.Add(organization);

            var updatedOrganization = new Organization
            {
                Name = "Org5 Updated",
                Email = "org5updated@example.com",
                Phone = "0987654321",
                Address = "New Address 127",
                Types = (Models.Type)(1)
            };

            // Act
            var result = await _repository.Update(updatedOrganization, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedOrganization.Name, result.Name);
            Assert.AreEqual(updatedOrganization.Email, result.Email);
        }

        [Test]
        public void Update_ShouldThrowCouldNotUpdateException_WhenOrganizationNotFound()
        {
            // Arrange
            var nonExistentOrganization = new Organization
            {
                Name = "NonExistentOrg",
                Email = "noemail@example.com",
                Phone = "0000000000",
                Address = "Unknown",
                Types = (Models.Type)(1)
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<CouldNotUpdateException>(async () => await _repository.Update(nonExistentOrganization, 999));
            Assert.AreEqual("Organization not found", exception.Message);
        }

        [Test]
        public async Task Delete_ShouldDeleteOrganizationSuccessfully()
        {
            // Arrange
            var organization = new Organization
            {
                UserId = 6,
                Name = "Org6",
                Email = "org6@example.com",
                Phone = "1234567895",
                Address = "128 Org Blvd",
                Types = (Models.Type)(1)
            };
            await _repository.Add(organization);

            // Act
            var result = await _repository.Delete(6);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(organization.Name, result.Name);
        }

        [Test]
        public void Delete_ShouldThrowCouldNotDeleteException_WhenOrganizationNotFound()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<CouldNotDeleteException>(async () => await _repository.Delete(999));
            Assert.AreEqual("Organization not found", exception.Message);
        }
    }
}

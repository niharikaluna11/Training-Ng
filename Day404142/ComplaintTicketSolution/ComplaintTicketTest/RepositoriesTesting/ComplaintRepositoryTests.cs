using ComplaintTicketAPI.Context;
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
    public class ComplaintRepositoryTests
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private ComplaintRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new ComplaintRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Add_ShouldAddComplaintSuccessfully()
        {
            // Arrange
            var complaint = new Complaint
            {
                Id = 1,
                Description = "Network issue",
                OrganizationId = 1,
                UserId = 1
            };

            // Act
            var result = await _repository.Add(complaint);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Network issue", result.Description);
        }

        [Test]
        public async Task Get_ShouldReturnComplaint_WhenExists()
        {
            // Arrange
            var complaint = new Complaint
            {
                Id = 2,
                Description = "Server issue",
                OrganizationId = 1,
                UserId = 1
            };
            await _repository.Add(complaint);

            // Act
            var result = await _repository.Get(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Server issue", result.Description);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenComplaintDoesNotExist()
        {
            // Act
            var result = await _repository.Get(99);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllComplaints()
        {
            // Arrange
            var complaint1 = new Complaint { Id = 3, Description = "Printer issue", OrganizationId = 1, UserId = 1 };
            var complaint2 = new Complaint { Id = 4, Description = "Network down", OrganizationId = 1, UserId = 1 };
            await _repository.Add(complaint1);
            await _repository.Add(complaint2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Update_ShouldUpdateComplaintSuccessfully()
        {
            // Arrange
            var complaint = new Complaint
            {
                Id = 5,
                Description = "Email issue",
                OrganizationId = 1,
                UserId = 1
            };
            await _repository.Add(complaint);

            var updatedComplaint = new Complaint
            {
                Id = 5,
                Description = "Updated email issue",
                OrganizationId = 1,
                UserId = 1
            };

            // Act
            var result = await _repository.Update(updatedComplaint, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated email issue", result.Description);
        }

        [Test]
        public async Task Update_ShouldReturnNull_WhenComplaintDoesNotExist()
        {
            // Arrange
            var nonExistentComplaint = new Complaint
            {
                Id = 999,
                Description = "Non-existent complaint",
                OrganizationId = 1,
                UserId = 1
            };

            // Act
            var result = await _repository.Update(nonExistentComplaint, 999);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Delete_ShouldDeleteComplaintSuccessfully()
        {
            // Arrange
            var complaint = new Complaint
            {
                Id = 6,
                Description = "Backup issue",
                OrganizationId = 1,
                UserId = 1
            };
            await _repository.Add(complaint);

            // Act
            var result = await _repository.Delete(6);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Backup issue", result.Description);
        }

        [Test]
        public async Task Delete_ShouldReturnNull_WhenComplaintDoesNotExist()
        {
            // Act
            var result = await _repository.Delete(999);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetComplaintsByOrganizationId_ShouldReturnComplaintsForOrganization()
        {
            // Arrange
            var complaint1 = new Complaint { Id = 7, Description = "Network issue", OrganizationId = 1, UserId = 1 };
            var complaint2 = new Complaint { Id = 8, Description = "Printer issue", OrganizationId = 1, UserId = 1 };
            var complaint3 = new Complaint { Id = 9, Description = "Server issue", OrganizationId = 2, UserId = 2 };

            await _repository.Add(complaint1);
            await _repository.Add(complaint2);
            await _repository.Add(complaint3);

            // Act
            var result = await _repository.GetComplaintsByOrganizationId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(c => c.OrganizationId == 1));
        }
    }
}

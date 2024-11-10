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
    public class ComplaintStatusRepositoryTests
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private ComplaintStatusRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new ComplaintStatusRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Add_ShouldAddComplaintStatusSuccessfully()
        {
            // Arrange
            var complaintStatus = new ComplaintStatus
            {
                Id = 1,
                Status=(Status)1
            };

            // Act
            var result = await _repository.Add(complaintStatus);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(complaintStatus.Id, result.Id);
        }

        [Test]
        public void Add_ShouldThrowException_WhenAdditionFails()
        {
            // Arrange: Inject invalid data if applicable to cause failure
            var invalidComplaintStatus = new ComplaintStatus(); // Missing required fields

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _repository.Add(invalidComplaintStatus), "not able to add complaint status");
        }

        [Test]
        public async Task Get_ShouldReturnComplaintStatus_WhenExists()
        {
           
            var complaintStatus = new ComplaintStatus
            {
                Id = 1,
                Status = (Status)1
            };

            // Act
            var result = await _repository.Add(complaintStatus);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(complaintStatus.Id, result.Id);
            await _repository.Add(complaintStatus);

          
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenComplaintStatusDoesNotExist()
        {
            // Act
            var result = await _repository.Get(99);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllComplaintStatuses()
        {
            // Arrange
            var status1 = new ComplaintStatus { Id = 3, Status = (Status)1 };
            var status2 = new ComplaintStatus { Id = 4, Status = (Status)1 };
            await _repository.Add(status1);
            await _repository.Add(status2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Update_ShouldUpdateComplaintStatusSuccessfully()
        {
            // Arrange
            var complaintStatus = new ComplaintStatus
            {
                Id = 5,
                Status = (Status)1
            };
            await _repository.Add(complaintStatus);

            var updatedStatus = new ComplaintStatus
            {
                Id = 5,
                Status = (Status)1
            };

            // Act
            var result = await _repository.Update(updatedStatus, 5);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Update_ShouldReturnNull_WhenComplaintStatusDoesNotExist()
        {
            // Arrange
            var nonExistentStatus = new ComplaintStatus
            {
                Id = 999,
                Status = (Status)1
            };

            // Act
            var result = await _repository.Update(nonExistentStatus, 999);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Delete_ShouldDeleteComplaintStatusSuccessfully()
        {
            // Arrange
            var complaintStatus = new ComplaintStatus
            {
                Id = 3,
                Status = (Status)1
            };
            await _repository.Add(complaintStatus);

            // Act
            var result = await _repository.Delete(6);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Delete_ShouldReturnNull_WhenComplaintStatusDoesNotExist()
        {
            // Act
            var result = await _repository.Delete(999);

            // Assert
            Assert.IsNull(result);
        }
    }
}

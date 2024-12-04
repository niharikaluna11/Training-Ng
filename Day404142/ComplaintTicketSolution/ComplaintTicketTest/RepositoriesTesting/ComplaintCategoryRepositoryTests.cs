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
    public class ComplaintCategoryRepositoryTests
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private ComplaintCategoryRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new ComplaintCategoryRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Add_ShouldAddComplaintCategorySuccessfully()
        {
            // Arrange
            var category = new ComplaintCategory
            {
                CategoryId = 1,
                Name = "Network Issue",
                Description = "Issues related to network connectivity"
            };

            // Act
            var result = await _repository.Add(category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Network Issue", result.Name);
        }

        [Test]
        public async Task Get_ShouldReturnComplaintCategory_WhenExists()
        {
            // Arrange
            var category = new ComplaintCategory
            {
                CategoryId = 2,
                Name = "Software Bug",
                Description = "Issues related to software"
            };
            await _repository.Add(category);

            // Act
            var result = await _repository.Get(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Software Bug", result.Name);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenComplaintCategoryDoesNotExist()
        {
            // Act
            var result = await _repository.Get(99);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllComplaintCategories()
        {
            // Arrange
            var category1 = new ComplaintCategory { CategoryId = 3, Name = "Hardware Issue", Description = "Hardware related issues" };
            var category2 = new ComplaintCategory { CategoryId = 4, Name = "Billing Issue", Description = "Billing related issues" };
            await _repository.Add(category1);
            await _repository.Add(category2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Update_ShouldUpdateComplaintCategorySuccessfully()
        {
            // Arrange
            var category = new ComplaintCategory
            {
                CategoryId = 5,
                Name = "Initial Category",
                Description = "Initial Description"
            };
            await _repository.Add(category);

            var updatedCategory = new ComplaintCategory
            {
                CategoryId = 5,
                Name = "Updated Category",
                Description = "Updated Description"
            };

            // Act
            var result = await _repository.Update(updatedCategory, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Category", result.Name);
        }

        [Test]
        public async Task Update_ShouldReturnNull_WhenComplaintCategoryDoesNotExist()
        {
            // Arrange
            var nonExistentCategory = new ComplaintCategory
            {
                CategoryId = 999,
                Name = "Non-Existent Category",
                Description = "This category does not exist"
            };

            // Act
            var result = await _repository.Update(nonExistentCategory, 999);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Delete_ShouldDeleteComplaintCategorySuccessfully()
        {
            // Arrange
            var category = new ComplaintCategory
            {
                CategoryId = 6,
                Name = "Delete Test Category",
                Description = "Description for deletion test"
            };
            await _repository.Add(category);

            // Act
            var result = await _repository.Delete(6);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete Test Category", result.Name);
        }

        [Test]
        public async Task Delete_ShouldReturnNull_WhenComplaintCategoryDoesNotExist()
        {
            // Act
            var result = await _repository.Delete(999);

            // Assert
            Assert.IsNull(result);
        }
    }
}

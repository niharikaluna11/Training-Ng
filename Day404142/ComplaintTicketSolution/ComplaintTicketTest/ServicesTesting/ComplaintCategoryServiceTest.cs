using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintTicketTest.ServicesTesting
{
    public class ComplaintCategoryServiceTest
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private ComplaintCategoryService _service;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<ComplaintCategoryService>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestComplaintCategoryDB")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ComplaintCategoryService>>();
            _service = new ComplaintCategoryService(_context, _mockMapper.Object, _mockLogger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllComplaintCategories_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<ComplaintCategory>
            {
                new ComplaintCategory { Id = 1, Name = "Category1" },
                new ComplaintCategory { Id = 2, Name = "Category2" }
            };

            _context.ComplaintCategories.AddRange(categories);
            await _context.SaveChangesAsync();

            _mockMapper.Setup(m => m.Map<IEnumerable<ComplaintCategoryResponseDTO>>(It.IsAny<IEnumerable<ComplaintCategory>>()))
                       .Returns(categories.Select(c => new ComplaintCategoryResponseDTO { Id = c.Id, Name = c.Name }));

            // Act
            var result = await _service.GetAllComplaintCategories();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task AddComplaintCategory_ShouldAddCategory()
        {
            // Arrange
            var categoryDto = new ComplaintCategoryDTO { Name = "NewCategory" };
            var category = new ComplaintCategory { Id = 1, Name = "NewCategory" };

            _mockMapper.Setup(m => m.Map<ComplaintCategory>(categoryDto)).Returns(category);
            _mockMapper.Setup(m => m.Map<ComplaintCategoryResponseDTO>(It.IsAny<ComplaintCategory>()))
                       .Returns(new ComplaintCategoryResponseDTO { Id = category.Id, Name = category.Name });

            // Act
            var result = await _service.AddComplaintCategory(categoryDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NewCategory", result.Name);
            Assert.AreEqual(1, _context.ComplaintCategories.Count());
        }

        [Test]
        public void GetAllComplaintCategories_WhenExceptionThrown_ShouldLogError()
        {
            // Arrange
            _mockMapper.Setup(m => m.Map<IEnumerable<ComplaintCategoryResponseDTO>>(It.IsAny<IEnumerable<ComplaintCategory>>()))
                       .Throws(new Exception("Test exception"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetAllComplaintCategories());
            Assert.AreEqual("An error occurred while fetching complaint categories.", ex.Message);
            _mockLogger.Verify(
                l => l.LogError(It.IsAny<Exception>(), "Error occurred while fetching complaint categories."),
                Times.Once);
        }

        [Test]
        public void AddComplaintCategory_WhenDbUpdateExceptionThrown_ShouldLogError()
        {
            // Arrange
            var categoryDto = new ComplaintCategoryDTO { Name = "FaultyCategory" };
            _mockMapper.Setup(m => m.Map<ComplaintCategory>(categoryDto)).Returns(new ComplaintCategory());

            _context.Database.EnsureDeleted();
            _mockMapper.Setup(m => m.Map<ComplaintCategoryResponseDTO>(It.IsAny<ComplaintCategory>()));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.AddComplaintCategory(categoryDto));
            Assert.AreEqual("An error occurred while adding a new complaint category.", ex.Message);
            _mockLogger.Verify(
                l => l.LogError(It.IsAny<Exception>(), "Error occurred while adding a new complaint category."),
                Times.Once);
        }
    }
}

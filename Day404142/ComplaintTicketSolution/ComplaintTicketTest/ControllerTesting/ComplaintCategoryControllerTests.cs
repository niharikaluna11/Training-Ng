//using ComplaintTicketAPI.Controllers;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using ComplaintTicketAPI.Context;
//using ComplaintTicketAPI.Services;  // Assuming ComplaintCategoryService is in this namespace
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using NUnit.Framework;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace ComplaintTicketApiTests.Controllers
//{
//    public class ComplaintCategoryControllerTests
//    {
//        private ComplaintCategoryController _complaintCategoryController;
//        private ComplaintTicketContext _context;
//        private Mock<IMapper> _mockMapper;
//        private Mock<ILogger<ComplaintCategoryService>> _mockLogger;

//        [SetUp]
      
//        public void Setup()
//        {
//            // Setup InMemoryDatabase
//            var options = new DbContextOptionsBuilder<ComplaintTicketContext>()
//                .UseInMemoryDatabase(databaseName: "ComplaintCategoryTestDb")
//                .Options;
//            _context = new ComplaintTicketContext(options);

//            // Mocking IMapper and ILogger
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<ComplaintCategoryService>>();

//            // Initialize the controller with mocked services
//            _complaintCategoryController = new ComplaintCategoryController(
//                new ComplaintCategoryService(_context, _mockMapper.Object, _mockLogger.Object)
//            );
//        }

//        [Test]
//        public async Task GetCategories_ShouldReturnCategories()
//        {
//            // Arrange
//            var category = new ComplaintCategory { Id = 1, Name = "Technical" };
//            await _context.ComplaintCategories.AddAsync(category);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _complaintCategoryController.GetCategories();

//            // Assert
//            var actionResult = result as ActionResult<IEnumerable<ComplaintCategoryResponseDTO>>;
//            Assert.IsNotNull(actionResult);

//            var okResult = actionResult.Result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            Assert.IsInstanceOf<List<ComplaintCategoryResponseDTO>>(okResult.Value);
//        }

//        [Test]
//        public async Task CreateCategory_ShouldReturnCreatedCategory()
//        {
//            // Arrange
//            var categoryDto = new ComplaintCategoryDTO { Name = "New Category" };

//            // Act
//            var result = await _complaintCategoryController.CreateCategory(categoryDto);

//            // Assert

//            var actionResult = result as ActionResult<ComplaintCategoryResponseDTO>;
//            Assert.IsNotNull(actionResult);

//            var createdResult = actionResult.Result as CreatedAtActionResult;
//            Assert.IsNotNull(createdResult);
//            Assert.AreEqual(201, createdResult.StatusCode);
//            var createdCategory = createdResult.Value as ComplaintCategoryResponseDTO;
//            Assert.IsNotNull(createdCategory);
//            Assert.AreEqual("New Category", createdCategory.Name);
//        }

//        [Test]
//        public async Task CreateCategory_ShouldReturnBadRequest_WhenInvalidCategory()
//        {
//            // Arrange
//            var invalidCategoryDto = new ComplaintCategoryDTO { Name = "" };
//            _complaintCategoryController.ModelState.AddModelError("Name", "Required");

//            // Act
//            var result = await _complaintCategoryController.CreateCategory(invalidCategoryDto);

//            // Assert

//            var actionResult = result as ActionResult<ComplaintCategoryResponseDTO>;
//            Assert.IsNotNull(actionResult);

//            var badRequestResult = actionResult.Result as BadRequestObjectResult;
//            Assert.IsNotNull(badRequestResult);
//            Assert.AreEqual(400, badRequestResult.StatusCode);
//        }
//    }
//}

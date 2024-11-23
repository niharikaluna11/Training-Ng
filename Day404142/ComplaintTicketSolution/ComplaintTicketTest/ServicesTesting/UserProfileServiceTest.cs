//using AutoMapper;
//using Moq;
//using NUnit.Framework;
//using ComplaintTicketAPI.Services;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using ComplaintTicketAPI.Interfaces;
//using ComplaintTicketAPI.EmailInterface;
//using Microsoft.Extensions.Logging;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Threading;
//using ComplaintTicketAPI.Context;
//using ComplaintTicketAPI.EmailModel;

//namespace ComplaintTicketTest.ServicesTesting
//{
//    public class UserProfileServiceTest
//    {
//        private Mock<ComplaintTicketContext> _mockContext;
//        private Mock<IEmailSender> _mockEmailSender;
//        private Mock<ILogger<UserProfileService>> _mockLogger;
//        private IMapper _mapper;
//        private UserProfileService _service;

//        [SetUp]
//        public void Setup()
//        {
//            _mockContext = new Mock<ComplaintTicketContext>();
//            _mockEmailSender = new Mock<IEmailSender>();
//            _mockLogger = new Mock<ILogger<UserProfileService>>();

//            // Configure AutoMapper
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<ProfileUpdateDTO, UserProfile>();
//            });
//            _mapper = config.CreateMapper();

//           _service = new UserProfileService(_mockContext.Object, _mockEmailSender.Object, _mockLogger.Object);
//        }

//        [Test]
//        public async Task GetProfile_ShouldReturnProfile_WhenProfileExists()
//        {
//            // Arrange
//            var userId = 1;
//            var profile = new UserProfile
//            {
//                UserId = userId,
//                FirstName = "John",
//                LastName = "Doe",
//                Address = "123 Main St",
//                DateOfBirth = DateTime.Parse("1990-01-01"),
//                Email = "john.doe@example.com",
//                Phone = "1234567890",
//                Preferences = "Default"
//            };

//            var mockSet = new Mock<DbSet<UserProfile>>();
//            mockSet.As<IQueryable<UserProfile>>().Setup(m => m.Provider).Returns(new[] { profile }.AsQueryable().Provider);
//            mockSet.As<IQueryable<UserProfile>>().Setup(m => m.Expression).Returns(new[] { profile }.AsQueryable().Expression);
//            mockSet.As<IQueryable<UserProfile>>().Setup(m => m.ElementType).Returns(new[] { profile }.AsQueryable().ElementType);
//            mockSet.As<IQueryable<UserProfile>>().Setup(m => m.GetEnumerator()).Returns(new[] { profile }.AsQueryable().GetEnumerator());

//            _mockContext.Setup(c => c.Profiles).Returns(mockSet.Object);

//            // Act
//            var result = await _service.GetProfile(userId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(userId, result.UserId);
//            Assert.AreEqual("John", result.FirstName);
//        }

//        [Test]
//        public async Task UpdateProfile_ShouldReturnUpdatedProfile_WhenProfileExists()
//        {
//            // Arrange
//            var userId = 1;
//            var updateDto = new ProfileUpdateDTO
//            {
//                FirstName = "Jane",
//                LastName = "Smith",
//                Address = "456 Elm St",
//                DateOfBirth = DateTime.Parse("1992-05-15"),
//                Email = "jane.smith@example.com",
//                Phone = "9876543210",
//                Preferences = "Updated"
//            };

//            var profile = new UserProfile
//            {
//                UserId = userId,
//                FirstName = "John",
//                LastName = "Doe",
//                Address = "123 Main St",
//                DateOfBirth = DateTime.Parse("1990-01-01"),
//                Email = "john.doe@example.com",
//                Phone = "1234567890",
//                Preferences = "Default"
//            };
///*
//            _mockContext.Setup(c => c.Profiles.FirstOrDefaultAsync(It.IsAny<Func<UserProfile, bool>>(), It.IsAny<CancellationToken>()))
//                        .ReturnsAsync(profile);*/
//            _mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

//            // Act
//            var result = await _service.UpdateProfile(userId, updateDto);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("Jane", result.FirstName);
//            Assert.AreEqual("Smith", result.LastName);
//            Assert.AreEqual("456 Elm St", result.Address);
//            Assert.AreEqual("jane.smith@example.com", result.Email);

//            _mockEmailSender.Verify(es => es.SendEmail(It.IsAny<Message>()), Times.Once);
//        }
//    }
//}

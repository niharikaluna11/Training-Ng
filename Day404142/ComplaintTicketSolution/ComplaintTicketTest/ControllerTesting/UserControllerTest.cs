//using ComplaintTicketAPI.Controllers;
//using ComplaintTicketAPI.Interfaces;
//using ComplaintTicketAPI.Models.DTO;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using NUnit.Framework;
//using System;
//using System.Threading.Tasks;
//using ComplaintTicketAPI.Models;

//namespace ComplaintTicketApiTests.Controllers
//{
//    public class UserControllerTest
//    {
//        private Mock<IUserService> _mockUserService;
//        private Mock<ILogger<User>> _mockLogger;
//        private UserController _userController;

//        [SetUp]
//        public void Setup()
//        {
//            _mockUserService = new Mock<IUserService>();
//            _mockLogger = new Mock<ILogger<User>>();
//            _userController = new UserController(_mockUserService.Object, _mockLogger.Object);
//        }

//        [Test]
//        public async Task UserRegistration_ShouldReturnOk_WhenRegistrationIsSuccessful()
//        {
//            // Arrange
//            var registerDto = new RegisterUserDto
//            {
//                Username = "testuser",
//                Email = "testuser@example.com",
//                Password = "Password123"
//            };

//            //var registeredUser = new User
//            //{
//            //    Username = "testuser",

//            //};

//            _mockUserService.Setup(service => service.Register(registerDto));
//                           // .ReturnsAsync(registeredUser);

//            // Act
//            var result = await _userController.UserRegistration(registerDto);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//           // Assert.AreEqual(registeredUser, okResult.Value);
//        }

//        [Test]
//        public async Task UserRegistration_ShouldReturnInternalServerError_WhenExceptionOccurs()
//        {
//            // Arrange
//            var registerDto = new RegisterUserDto
//            {
//                Username = "testuser",
//                Email = "testuser@example.com",
//                Password = "Password123"
//            };

//            _mockUserService.Setup(service => service.Register(registerDto))
//                            .ThrowsAsync(new Exception("Error during registration"));

//            // Act
//            var result = await _userController.UserRegistration(registerDto);

//            // Assert
//            var internalServerErrorResult = result as ObjectResult;
//            Assert.IsNotNull(internalServerErrorResult);
//            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
//            Assert.AreEqual("Cannot Register User.", internalServerErrorResult.Value);
//        }

//        [Test]
//        public async Task LoginUser_ShouldReturnOk_WhenAuthenticationIsSuccessful()
//        {
//            // Arrange
//            var loginDto = new LoginRequestDTO
//            {
//                Username="bhvcgc",
//                Password = "Password123"
//            };

//            var authenticatedUser = new LoginResponseDTO
//            {
//                Username = "testuser"
//            };

//            _mockUserService.Setup(service => service.Authenticate(loginDto))
//                            .ReturnsAsync(authenticatedUser);

//            // Act
//            var result = await _userController.LoginUser(loginDto);

//            // Assert
//            var okResult = result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            Assert.AreEqual(authenticatedUser, okResult.Value);
//        }

//        [Test]
//        public async Task LoginUser_ShouldReturnUnauthorized_WhenAuthenticationFails()
//        {
//            // Arrange
//            var loginDto = new LoginRequestDTO
//            {
//                Username = "testuser",
//                Password = "WrongPassword"
//            };

//            _mockUserService.Setup(service => service.Authenticate(loginDto))
//                            .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

//            // Act
//            var result = await _userController.LoginUser(loginDto);

//            // Assert
//            var unauthorizedResult = result as ObjectResult;
//            Assert.IsNotNull(unauthorizedResult);
//            Assert.AreEqual(401, unauthorizedResult.StatusCode);
//            Assert.AreEqual("Invalid credentials", unauthorizedResult.Value);
//        }




//    }
//}

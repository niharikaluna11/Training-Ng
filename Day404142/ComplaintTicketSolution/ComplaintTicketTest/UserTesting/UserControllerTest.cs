using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Controllers;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Tests
{
    public class UserControllerTest
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private Mock<ILogger<UserController>> _loggerController;
        private Mock<IUserService> _userServiceMock;
        private UserController _userController;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _loggerController = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();
            _mapper = new Mock<IMapper>();
        }

        // [Test]
        // [TestCase("TestUser", "test@gmail.com", "TestPassword", UserRole.Admin)]
        // public async Task RegisterUser_Successful(string username, string email, string password, UserRole role)
        // {
        //     // Arrange
        //     var newUser = new RegisterUserDto { UserName = username, UserEmail = email, Password = password, Role = role };
        //     var registeredUser = new User { UserName = username, UserEmail = email, Role = role };

        //     _userServiceMock.Setup(s => s.Register(It.IsAny<RegisterUserDto>()))
        //         .ReturnsAsync(registeredUser);

        //     // Act
        //     var result = await _userController.UserRegistration(newUser) as OkObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(200, result.StatusCode);
        //     Assert.AreEqual(registeredUser, result.Value);
        // }

        // [Test]
        // [TestCase("test@gmail.com", "TestPassword")]
        // public async Task LoginUser_Successful(string email, string password)
        // {
        //     // Arrange
        //     var loginRequest = new LoginRequestDTO { UserEmail = email, Password = password };
        //     var loginResponse = new LoginResponseDTO { Username = "TestUser" };

        //     _userServiceMock.Setup(s => s.Authenticate(It.IsAny<LoginRequestDTO>()))
        //         .ReturnsAsync(loginResponse);

        //     // Act
        //     var result = await _userController.LoginUser(loginRequest) as OkObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(200, result.StatusCode);
        //     Assert.AreEqual(loginResponse, result.Value);
        // }

        // [Test]
        // public async Task RegisterUser_InternalServerError()
        // {
        //     // Arrange
        //     var newUser = new RegisterUserDto { UserName = "ErrorUser", UserEmail = "error@gmail.com", Password = "password" };
        //     _userServiceMock.Setup(s => s.Register(It.IsAny<RegisterUserDto>()))
        //         .ThrowsAsync(new Exception("An error occurred"));

        //     // Act
        //     var result = await _userController.UserRegistration(newUser) as ObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        //     Assert.AreEqual("Cannot Register User.", result.Value);
        // }

        // [Test]
        // public async Task LoginUser_Unauthorized()
        // {
        //     // Arrange
        //     var loginRequest = new LoginRequestDTO { UserEmail = "test@gmail.com", Password = "wrongPassword" };
        //     _userServiceMock.Setup(s => s.Authenticate(It.IsAny<LoginRequestDTO>()))
        //         .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

        //     // Act
        //     var result = await _userController.LoginUser(loginRequest) as ObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(StatusCodes.Status401Unauthorized, result.StatusCode);
        //     Assert.AreEqual("Invalid credentials", result.Value);
        // }

        // [Test]
        // public async Task RegisterUser_BadRequest_InvalidEmail()
        // {
        //     // Arrange
        //     var newUser = new RegisterUserDto { UserName = "TestUser", UserEmail = "invalid-email", Password = "password" };
        //     _userController.ModelState.AddModelError("UserEmail", "Invalid email format");

        //     // Act
        //     var result = await _userController.UserRegistration(newUser) as BadRequestObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        // }

        // [Test]
        // public async Task LoginUser_BadRequest_MissingEmail()
        // {
        //     // Arrange
        //     var loginRequest = new LoginRequestDTO { Password = "password" };
        //     _userController.ModelState.AddModelError("UserEmail", "Email is required");

        //     // Act
        //     var result = await _userController.LoginUser(loginRequest) as BadRequestObjectResult;

        //     // Assert
        //     Assert.IsNotNull(result);
        //     Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        // }
    }
}

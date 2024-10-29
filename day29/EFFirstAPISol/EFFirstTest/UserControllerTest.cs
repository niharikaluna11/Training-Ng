using AutoMapper;
using EFFirstAPI.Context;
using EFFirstAPI.Controllers;
using EFFirstAPI.Interfaces;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;
using EFFirstAPI.Repositories;
using EFFirstAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EFFirstTest
{
    internal class UserControllerTest
    {
        private DbContextOptions<ShoppingContext> _options;
        private ShoppingContext _context;
        private UserRepository _repository;
        private UserService _service;
        private UserController _controller;
        private Mock<ILogger<UserController>> _loggerController;
        private Mock<ILogger<UserService>> _loggerService;
        private Mock<ILogger<UserRepository>> _loggerRepo;



        [SetUp]
        public void Setup()
        {

            // In-memory database setup
            _options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new ShoppingContext(_options);

            // Mocks for logging
            _loggerController = new Mock<ILogger<UserController>>();
            _loggerService = new Mock<ILogger<UserService>>();
            _loggerRepo = new Mock<ILogger<UserRepository>>();

            // Repository and Service setup
            _repository = new UserRepository(_context, _loggerRepo.Object);
            _service = new UserService(_repository, _loggerService.Object);

            // Controller setup
            _controller = new UserController(_loggerController.Object, _service);

        }

        [Test]
       // [TestCase("testuser1", "password1", Roles.User)]
        [TestCase("adminuser", "password2", Roles.Admin)]
        public async Task Register_ShouldReturnOk(string username, string password, Roles role)
        {
            // Arrange
            var userDto = new UserDTO
            {
                Username = username,
                Password = password,
                Role = role
            };

            // Act
            var result = await _controller.Register(userDto);
            Assert.IsNotNull(result);
            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase("adminuser", "password2", Roles.Admin)]
        public async Task Login_ShouldReturnOk(string username, string password, Roles role)
        {
            // Arrange
          
            var userDto = new UserDTO
            {
                Username = username,
                Password = password,
                Role = role
            };


            // Register user first (as a prerequisite)

            var result1 = await _controller.Register(userDto);
            Assert.IsNotNull(result1);

            var loginresponse = new LoginResponseDTO
            {
                Username = username,
                Password = password
            };
            
            var result2= await _controller.Login(loginresponse);
            Assert.IsNotNull(result2);

            // Assert
            var okResult = result2.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}

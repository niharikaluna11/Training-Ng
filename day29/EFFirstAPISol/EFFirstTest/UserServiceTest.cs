using EFFirstAPI.Context;
using EFFirstAPI.Models;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Repositories;
using EFFirstAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFirstTest
{
    internal class UserServiceTest
    {

        DbContextOptions options;
        ShoppingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        Mock<ILogger<UserService>> loggerUserService;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            repository = new UserRepository(context, loggerUserRepo.Object);
        }

        [Test]
        [TestCase("TestUser", "TestPassword", "TestHashKey", Roles.Admin)]
        public async Task TestAdd(string username, string password, string hashKey, Roles role)
        {
            var user = new UserDTO
            {
                Username = username,
                Password = password,
                Role = role
               
            };
            var userService = new UserService(repository, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [TestCase("TestUser1", "TestPassword1", "TestHashKey1")]
        public async Task TestAuthenticate(string username, string password, string hashKey)
        {
            var user = new UserDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = Roles.Admin
            };
            var userService = new UserService(repository, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            var loggedInUser = await userService.Autheticate(new LoginResponseDTO
            {
                Username = user.Username,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);
        }



    }
}

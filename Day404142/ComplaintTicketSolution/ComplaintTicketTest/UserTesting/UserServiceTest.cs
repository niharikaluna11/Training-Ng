using AutoMapper;
using ComplaintTicketAPI.Context;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Tests
{
    public class UserServiceTests
    {
        private Mock<IRepository<string, User>> _userRepoMock;
        private Mock<IRepository<int, UserProfile>> _profileRepoMock;
        private Mock<IRepository<int, Organization>> _organizationRepoMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private ComplaintTicketContext _context;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            // Initialize mocks
            _userRepoMock = new Mock<IRepository<string, User>>();
            _profileRepoMock = new Mock<IRepository<int, UserProfile>>();
            _organizationRepoMock = new Mock<IRepository<int, Organization>>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _tokenServiceMock = new Mock<ITokenService>();

            // Configure DbContext with in-memory database
            var options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ComplaintTicketContext(options);

            // Initialize UserService with mock dependencies
            _userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                _mapperMock.Object
            );
        }

        [Test]
        public async Task Authenticate_ShouldReturnLoginResponseDTO_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "testuser";
            var password = "password123";
            var hmac = new HMACSHA256();
            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var user = new User { Username = username, Password = hashedPassword, HashKey = hmac.Key, Roles = Role.User };

            _userRepoMock.Setup(repo => repo.Get(username)).ReturnsAsync(user);
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<UserTokenDTO>()))
                .ReturnsAsync("test_token");

            var loginUser = new LoginRequestDTO { Username = username, Password = password };

            // Act
            var result = await _userService.Authenticate(loginUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(username, result.Username);
            Assert.AreEqual("test_token", result.Token);
        }

        [Test]
        public void Authenticate_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            _userRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync((User)null);
            var loginUser = new LoginRequestDTO { Username = "nonexistentuser", Password = "password123" };

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _userService.Authenticate(loginUser));
        }

        [Test]
        public async Task Register_ShouldAddUserAndReturnLoginResponseDTO()
        {
            // Arrange
            var registerUser = new RegisterUserDto
            {
                Username = "newuser",
                Password = "newpassword123",
                Role = Role.User,
                Name = "New User",
                DateOfBirth = DateTime.UtcNow,
                Email = "newuser@example.com"
            };

            var hmac = new HMACSHA256();
            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));

            var user = new User
            {
                Username = registerUser.Username,
                Password = hashedPassword,
                HashKey = hmac.Key,
                Roles = registerUser.Role
            };

            _userRepoMock.Setup(repo => repo.Add(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _userService.Register(registerUser);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(registerUser.Username, result.Username);
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnListOfUserDTOs()
        {
            // Arrange
            var users = new List<User>
    {
        new User { Id = 1, Username = "user1",        HashKey = Encoding.UTF8.GetBytes("someHashKey1"),
            Password = Encoding.UTF8.GetBytes("somePasswordHash1")  },
        new User { Id = 2, Username = "user2",        HashKey = Encoding.UTF8.GetBytes("someHashKey2"), 
            Password = Encoding.UTF8.GetBytes("somePasswordHash2") }
    };

            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();

            var userDTOs = users.Select(u => new UserDTO { Id = u.Id, Username = u.Username });
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDTO>>(users)).Returns(userDTOs);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

    }
}

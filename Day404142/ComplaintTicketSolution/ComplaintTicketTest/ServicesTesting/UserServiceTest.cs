using ComplaintTicketAPI.Services;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Repositories;
using ComplaintTicketAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.EmailInterface;

namespace ComplaintTicketTest.ServicesTesting
{
    public class UserServiceTest
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private Mock<IRepository<string, User>> _userRepoMock;
        private Mock<IRepository<int, UserProfile>> _profileRepoMock;
        private Mock<IRepository<int, Organization>> _organizationRepoMock;
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IEmailSender> _emailSenderMock;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("ComplaintTicketTestDB")
                .Options;
            _context = new ComplaintTicketContext(_options);

            _userRepoMock = new Mock<IRepository<string, User>>();
            _profileRepoMock = new Mock<IRepository<int, UserProfile>>();
            _organizationRepoMock = new Mock<IRepository<int, Organization>>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _tokenServiceMock = new Mock<ITokenService>();
            _emailSenderMock = new Mock<IEmailSender>();
        }

        [Test]
        public async Task Authenticate_UserExists_ReturnsToken()
        {
            // Arrange
            var user = new User
            {
                Username = "TestUser",
                Password = new byte[] { 1, 2, 3 }, // mock password hash
                HashKey = new byte[] { 1, 2, 3 },
                Roles = Role.User
            };
            var loginRequest = new LoginRequestDTO { Username = "TestUser", Password = "TestPassword" };

            _userRepoMock.Setup(x => x.Get("TestUser")).ReturnsAsync(user);
            _tokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");

            var userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                null,
                _emailSenderMock.Object
            );

            // Act
            var result = await userService.Authenticate(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TestUser", result.Username);
            Assert.AreEqual("TestToken", result.Token);
        }

        [Test]
        public async Task Authenticate_UserNotFound_ThrowsException()
        {
            // Arrange
            var loginRequest = new LoginRequestDTO { Username = "NonExistentUser", Password = "TestPassword" };

            _userRepoMock.Setup(x => x.Get("NonExistentUser")).ReturnsAsync((User)null);

            var userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                null,
                _emailSenderMock.Object
            );

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await userService.Authenticate(loginRequest));
            Assert.AreEqual("User not found", exception.Message);
        }

        [Test]
        public async Task Register_SuccessfullyAddsUser()
        {
            // Arrange
            var registerUser = new RegisterUserDto
            {
                Username = "NewUser",
                Password = "Password123",
                Role = Role.User,
                Email = "newuser@example.com",
                Name = "New User"
            };

            var userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                null,
                _emailSenderMock.Object
            );

            _userRepoMock.Setup(x => x.Add(It.IsAny<User>())).ReturnsAsync(new User
            {
                Username = "NewUser",
                Password = new byte[] { 1, 2, 3 },
                HashKey = new byte[] { 1, 2, 3 },
                Roles = Role.User
            });

            // Act
            var result = await userService.Register(registerUser);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("NewUser", result.Username);
        }

        [Test]
        public async Task Register_FailsToAddUser_ThrowsException()
        {
            // Arrange
            var registerUser = new RegisterUserDto
            {
                Username = "NewUser",
                Password = "Password123",
                Role = Role.User,
                Email = "newuser@example.com",
                Name = "New User"
            };

            _userRepoMock.Setup(x => x.Add(It.IsAny<User>())).ThrowsAsync(new Exception("User could not be added"));

            var userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                null,
                _emailSenderMock.Object
            );

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await userService.Register(registerUser));
            Assert.AreEqual("User could not be added", exception.Message);
        }

        [Test]
        public async Task GetAllUsers_ReturnsUsers()
        {
            // Arrange
            var user = new User
            {
                Username = "TestUser",
                Password = new byte[] { 1, 2, 3 }, // mock password hash
                HashKey = new byte[] { 1, 2, 3 },
                Roles = Role.User
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userService = new UserService(
                _userRepoMock.Object,
                _profileRepoMock.Object,
                _organizationRepoMock.Object,
                _loggerMock.Object,
                _tokenServiceMock.Object,
                _context,
                null,
                _emailSenderMock.Object
            );

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
    }
}

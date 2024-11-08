using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using ComplaintTicketAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintTicketAPI.Context;

namespace ComplaintTicketAPI.Tests.Repositories
{
    public class UserRepositoryTest
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private UserRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new UserRepository(_context);
        }


        [Test]
        public async Task AddUser_ShouldAddUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                Username = "niharika1",
                Password = Encoding.UTF8.GetBytes("niharika1@123"),
                HashKey= Encoding.UTF8.GetBytes("test@123"),
                Roles = Role.User
            };

            // Act
            var addedUser = await _repository.Add(user);

            // Assert
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(user.Username, addedUser.Username);
        }

        [Test]
        public async Task AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var user = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };

            // Act: Add user for the first time
            await _repository.Add(user);

            // Assert: Try adding the same user again and expect an exception
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _repository.Add(user));
            Assert.AreEqual("User already exists. Please log in.", exception.Message);
        }


        [Test]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };
            await _repository.Add(user);

            // Act
            var retrievedUser = await _repository.Get("johnny123");

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.Username, retrievedUser.Username);
        }

        [Test]
        public async Task GetUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var user = await _repository.Get("nonexistentuser");

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task UpdateUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };
            var updatedUser = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharikanew@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };
            await _repository.Add(user);

            // Act
            var result = await _repository.Update(updatedUser, "johnny123");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedUser.Password, result.Password);
            Assert.AreEqual(updatedUser.Roles, result.Roles);
        }

        [Test]
        public async Task UpdateUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var updatedUser = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<CouldNotUpdateException>(async () => await _repository.Update(updatedUser, "nonexistentuser"));
            Assert.AreEqual("User not found.", exception.Message);
        }

        [Test]
        public async Task DeleteUser_ShouldDeleteUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };
            await _repository.Add(user);

            // Act
            var deletedUser = await _repository.Delete("johnny123");

            // Assert
            Assert.IsNotNull(deletedUser);
            Assert.AreEqual(user.Username, deletedUser.Username);
        }

        [Test]
        public async Task DeleteUser_ShouldThrowException_WhenUserNotFound()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<CouldNotDeleteException>(async () => await _repository.Delete("nonexistentuser"));
            Assert.AreEqual("User not found.", exception.Message);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnUsersList()
        {
            // Arrange
            var user1 = new User
            {
                Username = "niharika",
                Password = Encoding.UTF8.GetBytes("niharika@123"),
                HashKey = Encoding.UTF8.GetBytes("test@123"),
                Roles = (Role)1
            };
            var user2 = new User
            {
                Username = "johnny",
                Password = Encoding.UTF8.GetBytes("johnny@123"),
                HashKey = Encoding.UTF8.GetBytes("testjohnny@123"),
                Roles = (Role)1
            };
            await _repository.Add(user1);
            await _repository.Add(user2);

            // Act
            var users = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count());
        }

        [Test]
        public async Task GetAllUsers_ShouldThrowEmptyCollectionException_WhenNoUsersExist()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await _repository.GetAll());
            Assert.AreEqual("Users Collection Empty", exception.Message);
        }
    }
}

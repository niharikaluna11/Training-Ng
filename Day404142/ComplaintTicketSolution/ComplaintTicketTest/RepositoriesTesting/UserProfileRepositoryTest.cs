using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ComplaintTicketAPI.Context;

namespace ComplaintTicketAPI.Tests.Repositories
{
    public class UserProfileRepositoryTest
    {
        private DbContextOptions<ComplaintTicketContext> _options;
        private ComplaintTicketContext _context;
        private UserProfileRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new ComplaintTicketContext(_options);
            _repository = new UserProfileRepository(_context);
        }

        [Test]
        public async Task AddUserProfile_ShouldAddProfileSuccessfully()
        {
            // Arrange
            var profile = new UserProfile
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Address = "123 Main St",
                DateOfBirth = new DateTime(1990, 1, 1),
                Preferences = "Sample Preferences"
            };

            // Act
            var addedProfile = await _repository.Add(profile);

            // Assert
            Assert.IsNotNull(addedProfile);
            Assert.AreEqual(profile.UserId, addedProfile.UserId);
        }

        [Test]
        public async Task GetUserProfile_ShouldReturnProfile_WhenProfileExists()
        {
            // Arrange
            var profile = new UserProfile
            {
                UserId = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "0987654321",
                Address = "456 Elm St",
                DateOfBirth = new DateTime(1995, 5, 15),
                Preferences = "Sample Preferences"
            };
            await _repository.Add(profile);

            // Act
            var retrievedProfile = await _repository.Get(profile.UserId);

            // Assert
            Assert.IsNotNull(retrievedProfile);
            Assert.AreEqual(profile.UserId, retrievedProfile.UserId);
        }

        [Test]
        public async Task GetUserProfile_ShouldReturnNull_WhenProfileDoesNotExist()
        {
            // Act
            var profile = await _repository.Get(999);

            // Assert
            Assert.IsNull(profile);
        }

        [Test]
        public async Task UpdateUserProfile_ShouldUpdateProfileSuccessfully()
        {
            // Arrange
            var profile = new UserProfile
            {
                UserId = 3,
                FirstName = "Alice",
                LastName = "Brown",
                Email = "alice.brown@example.com",
                Phone = "1122334455",
                Address = "789 Oak St",
                DateOfBirth = new DateTime(1985, 12, 25),
                Preferences = "Initial Preferences"
            };
            await _repository.Add(profile);

            var updatedProfile = new UserProfile
            {
                UserId = 3,
                FirstName = "Alice",
                LastName = "Brown",
                Email = "alice.updated@example.com",
                Phone = "9988776655",
                Address = "789 Updated St",
                DateOfBirth = new DateTime(1985, 12, 25),
                Preferences = "Updated Preferences"
            };

            // Act
            var result = await _repository.Update(updatedProfile, profile.UserId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedProfile.Email, result.Email);
            Assert.AreEqual(updatedProfile.Address, result.Address);
        }

        [Test]
        public async Task UpdateUserProfile_ShouldThrowException_WhenProfileNotFound()
        {
            // Arrange
            var updatedProfile = new UserProfile
            {
                UserId = 999,
                FirstName = "Nonexistent",
                LastName = "User",
                Email = "nonexistent@example.com",
                Phone = "0000000000",
                Address = "No Address",
                DateOfBirth = DateTime.Now,
                Preferences = "No Preferences"
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _repository.Update(updatedProfile, 999));
            Assert.AreEqual("Not able to update profile", exception.Message);
        }

        [Test]
        public async Task GetAllUserProfiles_ShouldReturnProfilesList()
        {
            // Arrange
            var profile1 = new UserProfile
            {
                UserId = 4,
                FirstName = "User1",
                LastName = "Test1",
                Email = "user1@example.com",
                Phone = "1111111111",
                Address = "Address1",
                DateOfBirth = new DateTime(2000, 1, 1),
                Preferences = "Preferences1"
            };
            var profile2 = new UserProfile
            {
                UserId = 5,
                FirstName = "User2",
                LastName = "Test2",
                Email = "user2@example.com",
                Phone = "2222222222",
                Address = "Address2",
                DateOfBirth = new DateTime(1999, 2, 2),
                Preferences = "Preferences2"
            };
            await _repository.Add(profile1);
            await _repository.Add(profile2);

            // Act
            var profiles = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(profiles);
            Assert.AreEqual(2, profiles.Count());
        }

        [Test]
        public async Task DeleteUserProfile_ShouldDeleteProfileSuccessfully()
        {
            // Arrange
            var profile = new UserProfile
            {
                UserId = 6,
                FirstName = "Delete",
                LastName = "User",
                Email = "delete.user@example.com",
                Phone = "3333333333",
                Address = "Delete Address",
                DateOfBirth = new DateTime(1980, 1, 1),
                Preferences = "Preferences"
            };
            await _repository.Add(profile);

            // Act
            var deletedProfile = await _repository.Delete(profile.UserId);

            // Assert
            Assert.IsNotNull(deletedProfile);
            Assert.AreEqual(profile.UserId, deletedProfile.UserId);
        }

        [Test]
        public async Task DeleteUserProfile_ShouldThrowException_WhenProfileNotFound()
        {
            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () => await _repository.Delete(999));
            Assert.AreEqual("Not able to delete profile", exception.Message);
        }
    }
}

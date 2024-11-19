//using ComplaintTicketAPI.Services;
//using ComplaintTicketAPI.Interfaces;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using Moq;
//using NUnit.Framework;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading.Tasks;
//using ComplaintTicketAPI.EmailInterface;
//using ComplaintTicketAPI.EmailModel;

//namespace ComplaintTicketTest.ServicesTesting
//{
//    public class OrganizationProfileServiceTest
//    {
//        private Mock<IRepository<int, Organization>> _mockOrganizationRepo;
//        private Mock<ILogger<OrganizationProfileService>> _mockLogger;
//        private Mock<IEmailSender> _mockEmailSender;
//        private OrganizationProfileService _service;

//        [SetUp]
//        public void Setup()
//        {
//            _mockOrganizationRepo = new Mock<IRepository<int, Organization>>();
//            _mockLogger = new Mock<ILogger<OrganizationProfileService>>();
//            _mockEmailSender = new Mock<IEmailSender>();
//            _service = new OrganizationProfileService(_mockOrganizationRepo.Object, _mockLogger.Object, _mockEmailSender.Object);
//        }

//        [Test]
//        public async Task GetOrganizationProfile_ShouldReturnOrganization_WhenExists()
//        {
//            // Arrange
//            var userId = 1;
//            var organization = new Organization
//            {
//                Id = userId,
//                Name = "Test Organization",
//                Email = "test@org.com",
//                Phone = "1234567890",
//                Address = "Test Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            _mockOrganizationRepo.Setup(repo => repo.Get(userId)).ReturnsAsync(organization);

//            // Act
//            var result = await _service.GetOrganizationProfile(userId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(organization.Name, result.Name);
//            Assert.AreEqual(organization.Email, result.Email);
//        }

//        [Test]
//        public void GetOrganizationProfile_ShouldThrowException_WhenOrganizationNotFound()
//        {
//            // Arrange
//            var userId = 1;
//            _mockOrganizationRepo.Setup(repo => repo.Get(userId)).ThrowsAsync(new Exception("Organization not found"));

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetOrganizationProfile(userId));
//            Assert.AreEqual("An error occurred while retrieving the organization profile.", ex.Message);
//        }

//        [Test]
//        public async Task UpdateOrganizationProfile_ShouldReturnUpdatedOrganization_WhenSuccessful()
//        {
//            // Arrange
//            var userId = 1;
//            var existingOrganization = new Organization
//            {
//                Id = userId,
//                Name = "Test Organization",
//                Email = "test@org.com",
//                Phone = "1234567890",
//                Address = "Test Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            var updateDto = new OrganizationProfileDTO
//            {
//                Name = "Updated Organization",
//                Email = "updated@org.com",
//                Phone = "9876543210",
//                Address = "Updated Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            _mockOrganizationRepo.Setup(repo => repo.Get(userId)).ReturnsAsync(existingOrganization);
//            _mockOrganizationRepo.Setup(repo => repo.Update(It.IsAny<Organization>(), userId)).ReturnsAsync(existingOrganization);

//            // Act
//            var result = await _service.UpdateOrganizationProfile(userId, updateDto);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(updateDto.Name, result.Name);
//            Assert.AreEqual(updateDto.Email, result.Email);
//            _mockEmailSender.Verify(emailSender => emailSender.SendEmail(It.IsAny<Message>()), Times.Once);
//        }

//        [Test]
//        public void UpdateOrganizationProfile_ShouldThrowException_WhenOrganizationNotFound()
//        {
//            // Arrange
//            var userId = 1;
//            var updateDto = new OrganizationProfileDTO
//            {
//                Name = "Updated Organization",
//                Email = "updated@org.com",
//                Phone = "9876543210",
//                Address = "Updated Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            _mockOrganizationRepo.Setup(repo => repo.Get(userId)).ReturnsAsync((Organization)null);

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.UpdateOrganizationProfile(userId, updateDto));
//            Assert.AreEqual("An error occurred while updating the organization profile.", ex.Message);
//        }

//        [Test]
//        public void UpdateOrganizationProfile_ShouldThrowException_WhenEmailSendingFails()
//        {
//            // Arrange
//            var userId = 1;
//            var existingOrganization = new Organization
//            {
//                Id = userId,
//                Name = "Test Organization",
//                Email = "test@org.com",
//                Phone = "1234567890",
//                Address = "Test Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            var updateDto = new OrganizationProfileDTO
//            {
//                Name = "Updated Organization",
//                Email = "updated@org.com",
//                Phone = "9876543210",
//                Address = "Updated Address",
//                Types = (ComplaintTicketAPI.Models.Type)1
//            };

//            _mockOrganizationRepo.Setup(repo => repo.Get(userId)).ReturnsAsync(existingOrganization);
//            _mockOrganizationRepo.Setup(repo => repo.Update(It.IsAny<Organization>(), userId)).ReturnsAsync(existingOrganization);
//            _mockEmailSender.Setup(emailSender => emailSender.SendEmail(It.IsAny<Message>())).Throws(new Exception("Email error"));

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.UpdateOrganizationProfile(userId, updateDto));
//            Assert.AreEqual("An error occurred while updating the organization profile.", ex.Message);
//        }
//    }
//}

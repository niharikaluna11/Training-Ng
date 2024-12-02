//using Moq;
//using NUnit.Framework;
//using ComplaintTicketAPI.Services;
//using ComplaintTicketAPI.Models.DTO;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Interfaces;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using ComplaintTicketAPI.Context;

//namespace ComplaintTicketTest.ServicesTesting
//{
//    public class OrganizationServiceTest
//    {
//        private Mock<ComplaintTicketContext> _mockContext;
//        private Mock<IMapper> _mockMapper;
//        private Mock<ILogger<OrganizationService>> _mockLogger;
//        private OrganizationService _service;

//        [SetUp]
//        public void Setup()
//        {
//            _mockContext = new Mock<ComplaintTicketContext>();
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<OrganizationService>>();
//            _service = new OrganizationService(_mockContext.Object, _mockMapper.Object, _mockLogger.Object);
//        }

//        [Test]
//        public async Task GetAllOrganizationsAsync_ShouldReturnOrganizations_WhenSuccessful()
//        {
//            // Arrange
//            var organizationList = new List<Organization>
//            {
//                new Organization { Id = 1, Name = "Org 1", Email = "org1@example.com", Phone = "1234567890", Address = "Address 1" },
//                new Organization { Id = 2, Name = "Org 2", Email = "org2@example.com", Phone = "0987654321", Address = "Address 2" }
//            };

//            var organizationDtoList = new List<OrganizationDTO>
//            {
//                new OrganizationDTO { orgId = 1, Name = "Org 1"},
//                new OrganizationDTO {orgId = 2, Name = "Org 2"}
//            };

//         //   _mockContext.Setup(c => c.Organizations.ToListAsync()).ReturnsAsync(organizationList);
//            _mockMapper.Setup(m => m.Map<IEnumerable<OrganizationDTO>>(organizationList)).Returns(organizationDtoList);

//            // Act
//            var result = await _service.GetAllOrganizationsAsync();

//            // Assert
//            Assert.IsNotNull(result);
//          //  Assert.AreEqual(2, result.Count);
//            Assert.AreEqual("Org 1", result.First().Name);
//            Assert.AreEqual("Org 2", result.Last().Name);
//        }

//        [Test]
//        public void GetAllOrganizationsAsync_ShouldThrowException_WhenDbUpdateExceptionOccurs()
//        {
//            // Arrange
//         //   _mockContext.Setup(c => c.Organizations.ToListAsync()).ThrowsAsync(new DbUpdateException());

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetAllOrganizationsAsync());
//            Assert.AreEqual("An error occurred while retrieving the organizations.", ex.Message);
//        }

//        [Test]
//        public void GetAllOrganizationsAsync_ShouldThrowException_WhenGeneralExceptionOccurs()
//        {
//            // Arrange
//      //      _mockContext.Setup(c => c.Organizations.ToListAsync()).ThrowsAsync(new Exception("Database connection error"));

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetAllOrganizationsAsync());
//            Assert.AreEqual("An unexpected error occurred while retrieving the organizations.", ex.Message);
//        }

//        [Test]
//        public async Task GetAllOrganizationsAsync_ShouldReturnEmpty_WhenNoOrganizations()
//        {
//            // Arrange
//            var organizationList = new List<Organization>(); // Empty list

//         //   _mockContext.Setup(c => c.Organizations.ToListAsync()).ReturnsAsync(organizationList);
//            _mockMapper.Setup(m => m.Map<IEnumerable<OrganizationDTO>>(organizationList)).Returns(new List<OrganizationDTO>());

//            // Act
//            var result = await _service.GetAllOrganizationsAsync();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.IsEmpty(result);
//        }
//    }
//}

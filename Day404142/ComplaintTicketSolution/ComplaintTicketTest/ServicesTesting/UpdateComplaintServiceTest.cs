//using Moq;
//using NUnit.Framework;
//using ComplaintTicketAPI.Services;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using ComplaintTicketAPI.Interfaces;
//using Microsoft.Extensions.Logging;
//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ComplaintTicketTest.ServicesTesting
//{
//    public class UpdateComplaintServiceTest
//    {
//        private Mock<IComplaintRepository> _mockComplaintRepo;
//        private Mock<IMapper> _mockMapper;
//        private Mock<ILogger<UpdateComplaintService>> _mockLogger;
//        private UpdateComplaintService _service;

//        [SetUp]
//        public void Setup()
//        {
//            _mockComplaintRepo = new Mock<IComplaintRepository>();
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<UpdateComplaintService>>();
//            _service = new UpdateComplaintService(_mockComplaintRepo.Object, _mockMapper.Object, _mockLogger.Object);
//        }

//        [Test]
//        public async Task GetComplaintByOrganizationIdAsync_ShouldReturnComplaints_WhenSuccessful()
//        {
//            // Arrange
//            var orgId = 1;
//            var complaints = new List<Complaint>
//            {
//                new Complaint { Id = 1, OrganizationId = orgId, Description = "Complaint 1" },
//                new Complaint { Id = 2, OrganizationId = orgId, Description = "Complaint 2" }
//            };

//            _mockComplaintRepo.Setup(r => r.GetAll()).ReturnsAsync(complaints);

//            // Act
//            var result = await _service.GetComplaintByOrganizationIdAsync(orgId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(2, result.Count());
//            Assert.AreEqual("Complaint 1", result.First().Description);
//        }

//        [Test]
//        public void GetComplaintByOrganizationIdAsync_ShouldThrowException_WhenNoComplaintsFound()
//        {
//            // Arrange
//            var orgId = 1;
//            var complaints = new List<Complaint>();

//            _mockComplaintRepo.Setup(r => r.GetAll()).ReturnsAsync(complaints);

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetComplaintByOrganizationIdAsync(orgId));
//            Assert.AreEqual("An error occurred while retrieving complaints.", ex.Message);
//        }

//        [Test]
//        public async Task UpdateComplaintStatusAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO
//            {
//                ComplaintId = 1,
//                OrganizationId = 1,
//                StatusDate = DateTime.Now
//            };

//            var complaint = new Complaint
//            {
//                Id = 1,
//                OrganizationId = 1,
//                ComplaintStatusDates = new List<ComplaintStatusDate>()
//            };

//            _mockComplaintRepo.Setup(r => r.Get(updateRequest.ComplaintId)).ReturnsAsync(complaint);
//            _mockMapper.Setup(m => m.Map<ComplaintStatus>(updateRequest)).Returns(new ComplaintStatus());

//            _mockComplaintRepo.Setup(r => r.Update(It.IsAny<Complaint>(), complaint.Id)).ReturnsAsync(complaint);

//            // Act
//            var result = await _service.UpdateComplaintStatusAsync(updateRequest);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [Test]
//        public void UpdateComplaintStatusAsync_ShouldThrowException_WhenComplaintNotFound()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO
//            {
//                ComplaintId = 1,
//                OrganizationId = 1,
//                StatusDate = DateTime.Now
//            };

//            _mockComplaintRepo.Setup(r => r.Get(updateRequest.ComplaintId)).ReturnsAsync((Complaint)null);

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.UpdateComplaintStatusAsync(updateRequest));
//            Assert.AreEqual("Complaint not found for the provided organization.", ex.Message);
//        }

//        [Test]
//        public void UpdateComplaintStatusAsync_ShouldThrowException_WhenComplaintDoesNotBelongToOrganization()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO
//            {
//                ComplaintId = 1,
//                OrganizationId = 1,
//                StatusDate = DateTime.Now
//            };

//            var complaint = new Complaint { Id = 1, OrganizationId = 2 }; // Different organization ID
//            _mockComplaintRepo.Setup(r => r.Get(updateRequest.ComplaintId)).ReturnsAsync(complaint);

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.UpdateComplaintStatusAsync(updateRequest));
//            Assert.AreEqual("Complaint not found for this organization.", ex.Message);
//        }

//        [Test]
//        public void UpdateComplaintStatusAsync_ShouldThrowException_WhenAnErrorOccurs()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO
//            {
//                ComplaintId = 1,
//                OrganizationId = 1,
//                StatusDate = DateTime.Now
//            };

//            _mockComplaintRepo.Setup(r => r.Get(updateRequest.ComplaintId)).ThrowsAsync(new Exception("Unexpected error"));

//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.UpdateComplaintStatusAsync(updateRequest));
//            Assert.AreEqual("An error occurred while updating the complaint.", ex.Message);
//        }
//    }
//}

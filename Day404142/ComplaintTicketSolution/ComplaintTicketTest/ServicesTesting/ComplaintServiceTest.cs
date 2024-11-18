//using AutoMapper;
//using ComplaintTicketAPI.Context;
//using ComplaintTicketAPI.Interfaces;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using ComplaintTicketAPI.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ComplaintTicketTest.ServicesTesting
//{
//    public class ComplaintServiceTest
//    {
//        private DbContextOptions<ComplaintTicketContext> _options;
//        private ComplaintTicketContext _context;
//        private ComplaintService _service;
//        private Mock<IComplaintRepository> _mockComplaintRepository;
//        private Mock<IMapper> _mockMapper;
//        private Mock<IRepository<int, ComplaintStatus>> _mockComplaintStatusRepository;
//        private Mock<IRepository<int, ComplaintFile>> _mockComplaintFileRepository;
//        private Mock<IRepository<int, ComplaintStatusDate>> _mockComplaintStatusDateRepository;
//        private Mock<ILogger<ComplaintService>> _mockLogger;

//        [SetUp]
//        public void Setup()
//        {
//            _options = new DbContextOptionsBuilder<ComplaintTicketContext>()
//                .UseInMemoryDatabase("TestComplaintDB")
//                .Options;

//            _context = new ComplaintTicketContext(_options);
//            _mockComplaintRepository = new Mock<IComplaintRepository>();
//            _mockComplaintStatusRepository = new Mock<IRepository<int, ComplaintStatus>>();
//            _mockComplaintFileRepository = new Mock<IRepository<int, ComplaintFile>>();
//            _mockComplaintStatusDateRepository = new Mock<IRepository<int, ComplaintStatusDate>>();
//            _mockMapper = new Mock<IMapper>();
//            _mockLogger = new Mock<ILogger<ComplaintService>>();
//            _service = new ComplaintService(
//                _context,
//                _mockComplaintRepository.Object,
//                _mockComplaintStatusRepository.Object,
//                _mockComplaintFileRepository.Object,
//                _mockComplaintStatusDateRepository.Object,
//                _mockMapper.Object,
//                _mockLogger.Object
//            );
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            _context.Database.EnsureDeleted();
//            _context.Dispose();
//        }

//        [Test]
//        public async Task CreateComplaint_ShouldReturnCreatedComplaint()
//        {
//            // Arrange
//            var complaintDto = new CreateComplaintRequestDTO { Description = "Test Complaint" };
//            var complaint = new Complaint { Id = 1, Description = "Test Complaint" };
//            var complaintStatus = new ComplaintStatus { Id = 1, Status = Status.Recieved, Priority = Priority.Medium };
//            var complaintFile = new ComplaintFile { Id = 1, ComplaintId = 1 };
//            var complaintStatusDate = new ComplaintStatusDate { Id = 1, ComplaintId = 1, StatusDate = DateTime.Now };

//            _mockMapper.Setup(m => m.Map<Complaint>(complaintDto)).Returns(complaint);
//            _mockMapper.Setup(m => m.Map<ComplaintStatus>(complaintDto)).Returns(complaintStatus);
//            _mockMapper.Setup(m => m.Map<ComplaintFile>(complaintDto)).Returns(complaintFile);

//            _mockComplaintRepository.Setup(r => r.Add(It.IsAny<Complaint>())).ReturnsAsync(complaint);
//            _mockComplaintStatusRepository.Setup(r => r.Add(It.IsAny<ComplaintStatus>())).ReturnsAsync(complaintStatus);
//            _mockComplaintFileRepository.Setup(r => r.Add(It.IsAny<ComplaintFile>())).ReturnsAsync(complaintFile);
//            _mockComplaintStatusDateRepository.Setup(r => r.Add(It.IsAny<ComplaintStatusDate>())).ReturnsAsync(complaintStatusDate);

//            // Act
//            var result = await _service.CreateComplaint(complaintDto);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(1, result.Id);
//            Assert.AreEqual("Test Complaint", result.Description);
//        }

//        [Test]
//        public async Task GetComplaint_ShouldReturnComplaint()
//        {
//            // Arrange
//            var complaint = new Complaint { Id = 1, Description = "Test Complaint" };
//            _context.Complaints.Add(complaint);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _service.GetComplaint(1);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(1, result.Id);
//            Assert.AreEqual("Test Complaint", result.Description);
//        }

//        [Test]
//        public void GetComplaint_WhenNotFound_ShouldThrowException()
//        {
//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.GetComplaint(99));
//            Assert.AreEqual("An error occurred while retrieving the complaint.", ex.Message);
//            _mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), "Error occurred while retrieving the complaint with ID: 99"), Times.Once);
//        }

//        [Test]
//        public async Task GetAllComplaint_ShouldReturnAllComplaints()
//        {
//            // Arrange
//            var complaints = new List<Complaint>
//            {
//                new Complaint { Id = 1, Description = "Complaint 1" },
//                new Complaint { Id = 2, Description = "Complaint 2" }
//            };

//            _context.Complaints.AddRange(complaints);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _service.GetAllComplaint();

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(2, result.Count);
//        }

//        [Test]
//        public async Task TrackComplaintStatus_ShouldReturnComplaintStatus()
//        {
//            // Arrange
//            var complaint = new Complaint { Id = 1, Description = "Complaint 1" };
//            var complaintStatus = new ComplaintStatus { Id = 1, Status = Status.Recieved, Priority = Priority.Medium };
//            var complaintStatusDate = new ComplaintStatusDate { Id = 1, ComplaintId = 1, StatusDate = DateTime.Now, ComplaintStatus = complaintStatus };

//            _context.Complaints.Add(complaint);
//            _context.ComplaintStatusDates.Add(complaintStatusDate);
//            await _context.SaveChangesAsync();

//            _mockMapper.Setup(m => m.Map<ComplaintStatusDTO>(complaintStatus)).Returns(new ComplaintStatusDTO
//            {
//                Status = complaintStatus.Status,
//                Priority = complaintStatus.Priority,
//                StatusDate = complaintStatusDate.StatusDate
//            });

//            // Act
//            var result = await _service.TrackComplaintStatus(1);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(Status.Recieved, result.Status);
//            Assert.AreEqual(Priority.Medium, result.Priority);
//        }

//        [Test]
//        public void TrackComplaintStatus_WhenNotFound_ShouldThrowException()
//        {
//            // Act & Assert
//            var ex = Assert.ThrowsAsync<Exception>(async () => await _service.TrackComplaintStatus(99));
//            Assert.AreEqual("An error occurred while tracking the complaint status.", ex.Message);
//            _mockLogger.Verify(l => l.LogError(It.IsAny<Exception>(), "Error occurred while tracking status for complaint with ID: 99"), Times.Once);
//        }
//    }
//}

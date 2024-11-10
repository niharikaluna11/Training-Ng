using ComplaintTicketAPI.Controllers;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ComplaintTicketApiTests.Controllers
{
    public class ComplaintControllerTests
    {
        private Mock<IComplaintService> _mockComplaintService;
        private ComplaintTicketContext _mockContext;
        private Mock<IMapper> _mockMapper;
        private ComplaintController _complaintController;

        [SetUp]
        public void Setup()
        {
            _mockComplaintService = new Mock<IComplaintService>();
            _mockMapper = new Mock<IMapper>();

            // Setup InMemoryDatabase for ComplaintTicketContext
            var options = new DbContextOptionsBuilder<ComplaintTicketContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new ComplaintTicketContext(options);

            _complaintController = new ComplaintController(
                _mockComplaintService.Object,
                _mockContext,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task GetComplaint_ShouldReturnOk_WhenComplaintIsFound()
        {
            // Arrange
            var complaintId = 1;
            var complaint = new Complaint { Id = complaintId, Description = "Complaint description" };

            // Add to the InMemoryDatabase
            _mockContext.Complaints.Add(complaint);
            await _mockContext.SaveChangesAsync();

            _mockComplaintService.Setup(service => service.GetComplaint(complaintId)).ReturnsAsync(complaint);

            // Act
            var result = await _complaintController.GetComplaint(complaintId);

            // Assert
            var actionResult = result as ActionResult<Complaint>;
            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedComplaint = okResult.Value as Complaint;
            Assert.IsNotNull(returnedComplaint);
            Assert.AreEqual(complaint.Id, returnedComplaint.Id);
        }

        // Test for GetComplaint method (complaint not found)
        [Test]
        public async Task GetComplaint_ShouldReturnNotFound_WhenComplaintIsNotFound()
        {
            // Arrange
            var complaintId = 1;
            _mockComplaintService.Setup(service => service.GetComplaint(complaintId)).ReturnsAsync((Complaint)null);

            // Act
            var result = await _complaintController.GetComplaint(complaintId);

            var actionResult = result as ActionResult<Complaint>;
            Assert.IsNotNull(actionResult);
            // Assert
            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Complaint not found.", notFoundResult.Value);
        }

        // Test for CreateComplaint method (successful creation)
        [Test]
        public async Task CreateComplaint_ShouldReturnCreatedAtAction_WhenComplaintIsCreated()
        {
            // Arrange
            var complaintDto = new CreateComplaintRequestDTO { Description = "New Complaint" };
            var createdComplaint = new Complaint { Id = 1, Description = "New Complaint" };
            _mockComplaintService.Setup(service => service.CreateComplaint(complaintDto)).ReturnsAsync(createdComplaint);

            // Act
            var result = await _complaintController.CreateComplaint(complaintDto);

            // Assert

            var actionResult = result as ActionResult<Complaint>;
            Assert.IsNotNull(actionResult);
            var createdResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            var returnedComplaint = createdResult.Value as Complaint;
            Assert.IsNotNull(returnedComplaint);
            Assert.AreEqual(createdComplaint.Id, returnedComplaint.Id);
        }

        // Test for CreateComplaint method (bad request)
        [Test]
        public async Task CreateComplaint_ShouldReturnBadRequest_WhenComplaintDtoIsInvalid()
        {
            // Arrange
            var complaintDto = new CreateComplaintRequestDTO { Description = "" };
            _complaintController.ModelState.AddModelError("Description", "Required");

            // Act
            var result = await _complaintController.CreateComplaint(complaintDto);

            // Assert
            var actionResult = result as ActionResult<Complaint>;
            Assert.IsNotNull(actionResult);
            //var createdResult = actionResult.Result as CreatedAtActionResult;
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Invalid complaint data.", badRequestResult.Value);
        }

        // Test for TrackComplaintStatus method (successful tracking)
        [Test]
        public async Task TrackComplaintStatus_ShouldReturnOk_WhenComplaintStatusIsFound()
        {
            // Arrange
            var complaintId = 1;
            var complaint = new Complaint
            {
                Id = complaintId,
                ComplaintStatusDates = new List<ComplaintStatusDate>
                {
                    new ComplaintStatusDate { StatusDate = DateTime.Now, ComplaintStatus = new ComplaintStatus { Status = (Status)1} }
                }
            };

            _mockContext.Complaints.Add(complaint);
            await _mockContext.SaveChangesAsync();

            // Act
            var result = await _complaintController.TrackComplaintStatus(complaintId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var returnedStatus = okResult.Value as List<dynamic>;
            Assert.IsNotNull(returnedStatus);
            Assert.AreEqual(1, returnedStatus.Count);
        }

        // Test for GenerateComplaintReport method (successful report generation)
        [Test]
        public async Task GenerateComplainReport_ShouldReturnOk_WhenComplaintsAreFound()
        {
            // Arrange
            var orgId = 1;
            var complaints = new List<Complaint>
            {
                new Complaint { Id = 1, ComplaintStatusDates = new List<ComplaintStatusDate> { new ComplaintStatusDate { ComplaintStatus = new ComplaintStatus { Status = (Status)1 } } } },
                new Complaint { Id = 2, ComplaintStatusDates = new List<ComplaintStatusDate> { new ComplaintStatusDate { ComplaintStatus = new ComplaintStatus { Status = (Status)1 } } } }
            };

            _mockContext.Complaints.AddRange(complaints);
            await _mockContext.SaveChangesAsync();

            // Act
            var result = await _complaintController.GenerateComplainReport(orgId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var report = okResult.Value as dynamic;
            Assert.IsNotNull(report);
            Assert.AreEqual(2, report.TotalComplaints);
        }

        // Test for GenerateComplaintReport method (not found scenario)
        [Test]
        public async Task GenerateComplainReport_ShouldReturnNotFound_WhenComplaintsAreNotFound()
        {
            // Arrange
            var orgId = 1;
          //  _mockContext.Complaints.();  // Ensure no complaints in the database

            // Act
            var result = await _complaintController.GenerateComplainReport(orgId);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Complaints not found for the specified organization.", notFoundResult.Value);
        }
    }
}

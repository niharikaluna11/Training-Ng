//using Moq;
//using System;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using ComplaintTicketAPI.Controllers;
//using ComplaintTicketAPI.Interfaces;
//using ComplaintTicketAPI.Models;
//using ComplaintTicketAPI.Models.DTO;
//using Microsoft.AspNetCore.Mvc;
//using ComplaintTicketAPI.Interfaces.InteraceServices;

//namespace ComplaintTicketApiTests.Controllers
//{
//    public class UpdateComplaintControllerTests
//    {
//        private Mock<IUpdateComplaintService> _mockUpdateComplaintService;
//        private UpdateComplaintController _updateComplaintController;
//        private Mock<IComplaintDetailService> _mockComplaintDetailService;



//        [SetUp]
//        public void Setup()
//        {
//            _mockUpdateComplaintService = new Mock<IUpdateComplaintService>();
//            _mockComplaintDetailService = new Mock<IComplaintDetailService>();
//            _updateComplaintController = new UpdateComplaintController(_mockUpdateComplaintService.Object, _mockComplaintDetailService.Object);
//        }

//        [Test]
//        public async Task GetComplaint_ShouldReturnOk_WhenComplaintIsFound()
//        {
//            // Arrange
//            var orgId = 1;
//            var complaints = new List<Complaint>
//    {
//        new Complaint { Id = 1, Description = "Complaint description" }
//    };
//            _mockUpdateComplaintService
//                .Setup(service => service.GetComplaintByOrganizationIdAsync(orgId))
//                .ReturnsAsync(complaints.AsEnumerable());

//            // Act
//            var result = await _updateComplaintController.GetComplaint(orgId);

//            // Assert
//            Assert.IsNotNull(result);
//            var okResult = result.Result as OkObjectResult;
//            Assert.IsNotNull(okResult);
//            Assert.AreEqual(200, okResult.StatusCode);
//            var returnedComplaints = okResult.Value as IEnumerable<Complaint>;
//            Assert.IsNotNull(returnedComplaints);
//            Assert.AreEqual(complaints.FirstOrDefault().Id, returnedComplaints.FirstOrDefault()?.Id);
//        }

//        // Test for getting complaints successfully


//        //// Test for "Not Found" scenario when complaint is not found
//        //[Test]
//        //public async Task GetComplaint_ShouldReturnNotFound_WhenComplaintIsNotFound()
//        //{
//        //    // Arrange
//        //    var orgId = 1;
//        //    _mockUpdateComplaintService
//        //        .Setup(service => service.GetComplaintByOrganizationIdAsync(orgId))
//        //        .ThrowsAsync(new KeyNotFoundException("Complaint not found"));

//        //    // Act
//        //    var result = await _updateComplaintController.GetComplaint(orgId);

//        //    // Assert
//        //    Assert.IsNotNull(result);
//        //    var notFoundResult = result.Result as NotFoundObjectResult;
//        //    Assert.IsNotNull(notFoundResult);
//        //    Assert.AreEqual(404, notFoundResult.StatusCode);
//        //    Assert.AreEqual("Complaint not found", notFoundResult.Value);
//        //}




//        //[Test]
//        //public async Task UpdateComplaintStatus_ShouldReturnOk_WhenUpdateIsSuccessful()
//        //{
//        //    // Arrange
//        //    var updateRequest = new UpdateComplaintRequestDTO { ComplaintId = 1, Status = (Status)2 };
//        //    _mockUpdateComplaintService
//        //        .Setup(service => service.UpdateComplaintStatusAsync(updateRequest))
//        //        .ReturnsAsync(true); // Simulating successful update

//        //    // Act
//        //    var result = await _updateComplaintController.UpdateComplaintStatus(updateRequest);

//        //    // Assert
//        //    var okResult = result as OkResult;
//        //    Assert.IsNotNull(okResult);
//        //    Assert.AreEqual(200, okResult.StatusCode); // No Content (204) will still return Ok
//        //}

//        // Test for bad request when the status update fails
//        [Test]
//        public async Task UpdateComplaintStatus_ShouldReturnBadRequest_WhenUpdateFails()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO { ComplaintId = 1, Status = (Status)2 };
//            _mockUpdateComplaintService
//                .Setup(service => service.UpdateComplaintStatusAsync(updateRequest))
//                .ReturnsAsync(false); // Simulating failed update

//            // Act
//            var result = await _updateComplaintController.UpdateComplaintStatus(updateRequest);

//            // Assert
//            var badRequestResult = result as BadRequestObjectResult;
//            Assert.IsNotNull(badRequestResult);
//            Assert.AreEqual(400, badRequestResult.StatusCode);
//            Assert.AreEqual("Unable to update complaint status.", badRequestResult.Value);
//        }

//        // Test for "Not Found" when the complaint is not found while updating
//        [Test]
//        public async Task UpdateComplaintStatus_ShouldReturnNotFound_WhenComplaintNotFound()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO { ComplaintId = 1, Status = (Status)2 };
//            _mockUpdateComplaintService
//                .Setup(service => service.UpdateComplaintStatusAsync(updateRequest))
//                .ThrowsAsync(new KeyNotFoundException("Complaint not found"));

//            // Act
//            var result = await _updateComplaintController.UpdateComplaintStatus(updateRequest);

//            // Assert
//            var notFoundResult = result as NotFoundObjectResult;
//            Assert.IsNotNull(notFoundResult);
//            Assert.AreEqual(404, notFoundResult.StatusCode);
//            Assert.AreEqual("Complaint not found", notFoundResult.Value);
//        }

//        // Test for internal server error when an exception occurs
//        [Test]
//        public async Task UpdateComplaintStatus_ShouldReturnInternalServerError_WhenExceptionOccurs()
//        {
//            // Arrange
//            var updateRequest = new UpdateComplaintRequestDTO { ComplaintId = 1, Status = (Status)2 };
//            _mockUpdateComplaintService
//                .Setup(service => service.UpdateComplaintStatusAsync(updateRequest))
//                .ThrowsAsync(new Exception("Unexpected error"));

//            // Act
//            var result = await _updateComplaintController.UpdateComplaintStatus(updateRequest);

//            // Assert
//            var internalServerErrorResult = result as ObjectResult;
//            Assert.IsNotNull(internalServerErrorResult);
//            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
//            Assert.AreEqual("Internal server error: Unexpected error", internalServerErrorResult.Value);
//        }
//    }
//}

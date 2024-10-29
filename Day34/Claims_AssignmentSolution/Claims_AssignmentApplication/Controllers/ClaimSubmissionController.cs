using Claims_AssignmentApplication.Interfaces;
using Claims_AssignmentApplication.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Claims_AssignmentApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimSubmissionController : ControllerBase
    {
        private readonly IClaimService _claimService;
        private readonly IDocumentService _documentService;
        private readonly ILogger<ClaimSubmissionController> _logger;

        public ClaimSubmissionController(IClaimService claimService, IDocumentService documentService, ILogger<ClaimSubmissionController> logger)
        {
            _claimService = claimService;
            _documentService = documentService;
            _logger = logger;
        }

        // GET: api/ClaimSubmission/GetAllClaims
        [HttpGet("GetAllClaims")]
        public async Task<IActionResult> GetAllClaims()
        {
            try
            {
                var claims = await _claimService.GetAllClaims();
                return Ok(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving claims");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving claims");
            }
        }

        // POST: api/ClaimSubmission/SubmitClaim
        [HttpPost("SubmitClaim")]
        public async Task<IActionResult> SubmitClaim([FromBody] ClaimRequestDTO claimRequest)
        {
            if (!claimRequest.TermsAccepted)
            {
                return BadRequest("You must accept the terms and conditions to submit the claim.");
            }

            try
            {
                // Create the claim
                var createdClaim = await _claimService.CreateClaim(claimRequest);
                _logger.LogInformation("Claim created with ID: {ClaimId}", createdClaim);

                return Ok(createdClaim);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting claim");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error submitting claim");
            }
        }

        // POST: api/ClaimSubmission/UploadDocument/{claimId}
        [HttpPost("UploadDocument/{claimId}")]
        public async Task<IActionResult> UploadDocument(int claimId, [FromBody] DocumentUploadDTO documentUpload)
        {
            try
            {
                // Check if the claim exists
                var claim = await _claimService.GetClaim(claimId);
                if (claim == null)
                {
                    return NotFound("Claim not found");
                }

                // Associate the document with the claim ID
                documentUpload.ClaimId = claimId;

                // Create the document entry
                await _documentService.CreateDocument(documentUpload);
                _logger.LogInformation("Document uploaded for Claim ID: {ClaimId}", claimId);

                return Ok(new { Message = "Document uploaded successfully", ClaimId = claimId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading document");
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportClaimApplication.Interfaces;
using ReportClaimApplication.Models.DTO;
using System.Threading.Tasks;

namespace ReportClaimApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // POST: api/claim
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromForm] ClaimRequestDTO claimRequest, IFormFile[] documents, bool termsAccepted)
        {
            // Validate terms acceptance
            if (!termsAccepted)
            {
                return BadRequest("You must accept the terms and conditions to proceed with the claim.");
            }

            // Validate document uploads (ensure that the documents are valid)
            if (documents == null || documents.Length == 0)
            {
                return BadRequest("At least one document must be uploaded to create a claim.");
            }

            // Map document uploads to the claim request DTO
            foreach (var document in documents)
            {
                // Save the documents and add URLs to the DTO
                claimRequest.Documents.Add(new DocumentUploadDTO
                {
                    DocumentName = document.FileName,
                    DocumentUrl = await UploadDocumentAsync(document)
                });
            }

            // Create the claim
            var claimResponse = await _claimService.CreateClaimAsync(claimRequest);

            // Provide a message about successful creation
            return CreatedAtAction(nameof(GetClaimById), new { claimId = claimResponse.ClaimId },
                new { message = "Claim created successfully.", claim = claimResponse });
        }

        // GET: api/claim/{id}
        [HttpGet("{GetAllClaimByClaimantId}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
            {
                return NotFound(new { message = $"Claim with ID {id} not found." });
            }
            return Ok(new { message = "Claim details retrieved successfully.", claim });
        }

        // GET: api/claim
        [HttpGet]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(new { message = "All claims retrieved successfully.", claims });
        }

        // Helper method to handle document uploads
        private async Task<string> UploadDocumentAsync(IFormFile document)
        {
            
            return await Task.FromResult("uploaded-document-url"); // Placeholder
        }
    }
}

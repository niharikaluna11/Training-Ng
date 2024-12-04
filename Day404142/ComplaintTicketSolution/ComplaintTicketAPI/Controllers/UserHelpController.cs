using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHelpController : ControllerBase
    {
        private readonly IUserHelpService _userHelpService;

        public UserHelpController(IUserHelpService userHelpService)
        {
            _userHelpService = userHelpService;
        }

        // GET api/userhelp (Get all queries)
        [HttpGet("GetAllQueries")]
        public async Task<ActionResult<IEnumerable<UserHelp>>> GetAllQueries()
        {
            var queries = await _userHelpService.GetAllQueriesAsync();
            return Ok(queries);
        }

        // GET api/userhelp/{id} (Get specific query by ID)
        [HttpGet("GetQueryByEmail")]
        public async Task<ActionResult<UserHelp>> GetQueryByEmail(string email)
        {
            var query = await _userHelpService.GetQueryByEmailifAsync(email);
            if (query == null)
                return NotFound();
            return Ok(query);
        }

        // POST api/userhelp (Create new query)
        [HttpPost("SubmitQuery")]
      
        public async Task<ActionResult<UserHelp>> SubmitQuery(UserHelpdto userHelpDto)
        {
            if (string.IsNullOrEmpty(userHelpDto.email) || string.IsNullOrEmpty(userHelpDto.query))
                return BadRequest("Email and query are required.");

            // Map DTO to UserHelp model
            var userHelp = new UserHelp
            {
                email = userHelpDto.email,
                query = userHelpDto.query,
                IsResponded = false, // Default value
                AdminResponse = null // Default value
            };

            try
            {
                await _userHelpService.AddQueryAsync(userHelp);

                // Return the created resource URI and userHelp details
                return CreatedAtAction(nameof(GetQueryByEmail), new { email = userHelp.email }, userHelp);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // PUT api/userhelp/{id} (Admin responds to a query)
        [HttpPut("RespondToQuery")]
       
        public async Task<IActionResult> RespondToQuery(string email, [FromForm] string adminResponse)
        {
            if (string.IsNullOrEmpty(adminResponse))
                return BadRequest("Admin response cannot be empty.");

            try
            {
                await _userHelpService.UpdateQueryAsync(email, adminResponse);
                var updatedQuery = await _userHelpService.GetQueryByEmailifAsync(email);
                return Ok(new { message = "Response has been successfully added.", data = updatedQuery });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

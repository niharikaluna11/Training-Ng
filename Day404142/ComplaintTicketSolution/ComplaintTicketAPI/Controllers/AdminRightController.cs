using ComplaintTicketAPI.Exceptions;
using ComplaintTicketAPI.Interfaces.InteraceServices;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRightController : ControllerBase
    {
        private readonly IUserService _userService;  // Assuming IUserService is injected to interact with your User entity

        // Constructor to inject the IUserService
        public AdminRightController(IUserService userService)
        {
            _userService = userService;
        }

        // Reactivate user (soft delete)
        [HttpPut("reactivate")]
        public async Task<IActionResult> ReactivateUser(string key)
        {
            try
            {
                var user = await _userService.ReactivateUserAsync(key);
                return Ok(new
                {
                    Success = true,
                    Message = "User reactivated successfully.",
                    Data = user
                });
            }
            catch (CouldNotDeleteException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while reactivating the user.",
                    Error = ex.Message
                });
            }
        }

        // Soft delete user
        [HttpDelete("Deactivating")]
        public async Task<IActionResult> SoftDeleteUser(string key)
        {
            try
            {
                var user = await _userService.SoftDeleteUserAsync(key);
                return Ok(new
                {
                    Success = true,
                    Message = "User deactivated successfully.",
                    Data = user
                });
            }
            catch (CouldNotDeleteException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while deactivating the user.",
                    Error = ex.Message
                });
            }
        }

    }
}


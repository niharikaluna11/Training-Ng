using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost("send-reset-link")]
        public async Task<IActionResult> SendPasswordResetLink(string UsernameorEmail)
        {
            try
            {
                await _forgotPasswordService.SendPasswordResetLink(UsernameorEmail);
                return Ok(new { Message = "Password reset link with OTP has been sent to your email." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyResetOtp(string otp)
        {
            try
            {
                bool isValid = await _forgotPasswordService.VerifyResetOtp(otp);
                if (!isValid)
                {
                    return BadRequest(new { Error = "Invalid or expired OTP." });
                }
                return Ok(new { Message = "OTP is valid." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordDto resetRequest)
        {
            try
            {
                await _forgotPasswordService.ResetPassword(resetRequest.UsernameOrEmail, resetRequest.Otp, resetRequest.NewPassword, resetRequest.ConfirmNewPassword);
                return Ok(new { Message = "Password has been reset successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }

}

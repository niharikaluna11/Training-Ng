using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
using Microsoft.AspNetCore.Mvc;

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

            var response = await _forgotPasswordService.SendPasswordResetLink(UsernameorEmail);
            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            if (response is BaseResponseDTO successResponse)
            {
                return Ok(successResponse);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
            {
                Success = false,
                ErrorMessage = "An unexpected error occurred.",
                ErrorCode = 500
            });
        }

        [HttpGet("verify-otp")]
        public async Task<IActionResult> VerifyResetOtp(string otp)
        {

            var response = await _forgotPasswordService.VerifyResetOtp(otp);
            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            if (response is BaseResponseDTO successResponse)
            {
                return Ok(successResponse);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
            {
                Success = false,
                ErrorMessage = "An unexpected error occurred.",
                ErrorCode = 500
            });


        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordDto resetRequest)
        {

            var response = await _forgotPasswordService.ResetPassword(resetRequest.UsernameOrEmail, resetRequest.Otp, resetRequest.NewPassword, resetRequest.ConfirmNewPassword);
            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            if (response is BaseResponseDTO successResponse)
            {
                return Ok(successResponse);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
            {
                Success = false,
                ErrorMessage = "An unexpected error occurred.",
                ErrorCode = 500
            });
        }
    }

}

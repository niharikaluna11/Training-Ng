using ComplaintTicketAPI.Interfaces.InteraceServices;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models.DTO.ResponseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Type = ComplaintTicketAPI.Models.Type;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        //private readonly OtpService _otpService;
        private readonly IUserService _userService;
        private readonly ILogger<User> _logger;

        public UserController(IUserService userService, ILogger<User> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        // ILogger<Student> logger

        [HttpGet("GetUserIdByUsername")]
        public async Task<IActionResult> GetUserIdByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new { Message = "Username cannot be empty." });
            }

            try
            {
                // Assume GetUserByUsernameAsync fetches a user object by username
                var user = await _userService.GetUserByUsername(username);

                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }

                return Ok(new { UserId = user.Id, FirstName = user.Username });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }



        [HttpPost("RegistrationOFUser")]
       
        public async Task<IActionResult> UserRegistration(RegisterUserDto entity)
        {
            
            var response = await _userService.Register(entity);
            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            if (response is SuccessResponseDTO<LoginResponseDTO> successResponse)
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


        [HttpPost("send-otp")]
        public async Task<IActionResult> SendRegistrationOtp(EmailDTO emailDto)
        {
            var response = await _userService.SendRegistrationOtp(emailDto.Email);
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

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyRegistrationOtp( string email, string otp)
        {
            var response = await _userService.VerifyRegistrationOtp(email, otp);

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



        [HttpPost("LoginOFUser")]
        public async Task<IActionResult> LoginUser(LoginRequestDTO entity)
        {
            var response = await _userService.Authenticate(entity);

            if (response is ErrorResponseDTO errorResponse)
            {
                return StatusCode(errorResponse.ErrorCode, errorResponse);
            }

            if (response is SuccessResponseDTO<LoginResponseDTO> successResponse)
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

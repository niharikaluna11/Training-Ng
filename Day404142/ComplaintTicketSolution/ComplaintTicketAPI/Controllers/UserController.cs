using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Type = ComplaintTicketAPI.Models.Type;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILogger<User> _logger;

        public UserController(IUserService userService, ILogger<User> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        // ILogger<Student> logger

        [HttpGet("AvailableRoles")]
        public IActionResult GetAvailableRoles()
        {
            var roles = Enum.GetValues(typeof(Role));
            List<String> roleInString = new List<String>();
            int count = 0;
            foreach (var item in roles)
            {
                string itemS = item.ToString() + " " + count;
                roleInString.Add(itemS);
                count++;
            }
            return Ok(roleInString);
        }
        [HttpGet("AvailableTypesOFOrganization")]
        public IActionResult GetAvailableTypes()
        {
            var types = Enum.GetValues(typeof(Type));
            List<String> typeInString = new List<String>();
            int count = 0;
            foreach (var item in types)
            {
                string itemS = item.ToString() + " " + count;
                typeInString.Add(itemS);
                count++;
            }
            return Ok(typeInString);
        }


        [HttpPost("RegistrationOFUser")]
        public async Task<IActionResult> UserRegistration(RegisterUserDto entity)
        {
            try
            {
                var newUser = await _userService.Register(entity);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Register User.");
            }
        }

        [HttpPost("LoginOFUser")]
        public async Task<IActionResult> LoginUser(LoginRequestDTO entity)
        {
            try
            {
                var newUser = await _userService.Authenticate(entity);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
        }

    }
}

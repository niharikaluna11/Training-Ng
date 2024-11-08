using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComplaintTicketAPI.Interfaces;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Type = ComplaintTicketAPI.Models.Type;
using ComplaintTicketAPI.Services;

namespace ComplaintTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //organization controller
    //user can see the organziations and their id (with type too)
    //organization have
    public class GeneralController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;

        public GeneralController(IOrganizationService organizationService, IUserService userService)
        {
            _organizationService = organizationService;
            _userService = userService;
        }

        [HttpGet("User/AvailableRoles")]
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
        [HttpGet("Organization/AvailableTypesOFOrganization")]
        public IActionResult GetAvailableTypes()
        {
            var types = Enum.GetValues(typeof(Type));
            List<String> typeInString = new List<String>();
            int count = 1;
            foreach (var item in types)
            {
                string itemS = item.ToString() + " " + count;
                typeInString.Add(itemS);
                count++;
            }
            return Ok(typeInString);
        }

        [HttpGet("User/GetAllAvailableOrganizations")]

        public async Task<IActionResult> GetAllOrganizations()
        {
            var organizations = await _organizationService.GetAllOrganizationsAsync();

            if (organizations == null)
            {
                return NotFound("No organizations found.");
            }

            return Ok(organizations);
        }

        [HttpGet("Admin/GetAllAvailableUsers")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUsersAsync();

            if (users == null)
            {
                return NotFound("No Users found.");
            }

            return Ok(users);
        }

        [HttpGet("Complaint/GetAvailableCategories")]
        public IActionResult GetAvailableCategories()
        {
            var categories = Enum.GetValues(typeof(Category));
            List<String> categoryInString = new List<String>();
            int count = 0;
            foreach (var item in categories)
            {
                string itemS = item.ToString() + " " + count;
                categoryInString.Add(itemS);
                count++;
            }
            return Ok(categoryInString);
        }

        [HttpGet("Complaint/GetAvailablePriorityType")]
        public IActionResult GetAvailablePriority()
        {
            var priority = Enum.GetValues(typeof(Priority));
            List<String> PriorityInString = new List<String>();
            int count = 0;
            foreach (var item in priority)
            {
                string itemS = item.ToString() + " " + count;
                PriorityInString.Add(itemS);
                count++;
            }
            return Ok(PriorityInString);
        }

    }
}

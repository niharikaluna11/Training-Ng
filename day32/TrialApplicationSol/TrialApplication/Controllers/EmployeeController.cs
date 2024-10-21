using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }


    
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                var employeeid = await _employeeService.CreateEmployee(employee);
                _logger.LogInformation("Employee Added");
                return Ok(employeeid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var products = await _employeeService.GetAllEmployees();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getEmployeeNames")]
        public async Task<IActionResult> GetEmployeeNames()
        {
            try
            {
                var emp = await _employeeService.GetAllEmployees();
                //LINQ query
                var empname = (from empnames in emp
                               orderby empnames.Name 
                               select empnames.Name ).ToList();
                return Ok(empname);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("getEmployeeCount")]
        public async Task<IActionResult> GetEmployeeCount()
        {
            try
            {
                var emp = await _employeeService.GetAllEmployees();
                //LINQ query
                var empcount = (  from empnames in emp
                               select empnames.Name).Count();
                return Ok(empcount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


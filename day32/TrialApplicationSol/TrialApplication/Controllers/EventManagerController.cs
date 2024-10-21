using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using TrialApplication.Interfaces;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventManagerController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventController> _logger;

        private readonly IEmployeeService _employeeService;
        private readonly IBookingService _bookingService;

        public EventManagerController(IEventService eventService, IEmployeeService employeeService, IBookingService bookingService,ILogger<EventController> logger)
        {
            _eventService = eventService;
            _employeeService = employeeService;
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet("get_EventWith_Employee")]
        public async Task<IActionResult> GetEventWithEMployee()
        {
            try
            {
                var events = await _eventService.GetAllEvents();
                var employees = await _employeeService.GetAllEmployees();
                var bookings = await _bookingService.GetAllBookings();
                //LINQ query
             
                //SELECT e.EventName, ee.Name
                /*FROM Events e
                JOIN Bookings b ON b.EventId = e.EventId
                JOIN Employees ee ON b.EmployeeId = ee.Id;*/
               
                var details = ( from e in events 
                                join  b in bookings on e.EventId equals b.EventId
                                join ee in employees on b.EmployeeId equals ee.Id
                                select new {eventt = e.EventName,employye=ee.Name }
                                    ).ToList();
                return Ok(details);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

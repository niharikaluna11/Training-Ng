using Microsoft.AspNetCore.Mvc;
using TrialApplication.Interfaces;
using TrialApplication.Models.DTO;

namespace TrialApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {

        private readonly IEventService _eventService;
        private readonly ILogger<EventController> _logger;

        public EventController(IEventService eventService, ILogger<EventController> logger)
        {
            _eventService= eventService;
            _logger = logger;
        }



        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventDTO eventDTO)
        {
            try
            {
                var eventID = await _eventService.CreateEvent(eventDTO);
                _logger.LogInformation("Event Added");
                return Ok(eventID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("getAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            try
            {
                var events = await _eventService.GetAllEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}

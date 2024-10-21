using Microsoft.AspNetCore.Mvc;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models.DTO;

namespace TrialApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("BookEvent")]

        public async Task<IActionResult> BookEvent( BookingDTO bookingDto)
        {
           

          try
            {
                var bookingId = await _bookingService.BookEvent(bookingDto);
                return Ok(new { BookingId = bookingId });
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       
    }
}

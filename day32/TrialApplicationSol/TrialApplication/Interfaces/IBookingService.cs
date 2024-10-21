using TrialApplication.Models.DTO;
using TrialApplication.Models;

namespace TrialApplication.Interfaces
{
    public interface IBookingService
    {
        Task<int> BookEvent(BookingDTO booking);
        Task<IEnumerable<Booking>> GetAllBookings();
    }
}

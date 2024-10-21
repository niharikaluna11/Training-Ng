using TrialApplication.Models.DTO;
using TrialApplication.Models;

namespace TrialApplication.Interfaces
{
    public interface IEventService
    {
        Task<int> CreateEvent(EventDTO eventDTO);

        Task<IEnumerable<Event>> GetAllEvents();
    }
}

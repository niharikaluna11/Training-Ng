using AutoMapper;
using TrialApplication.Exceptions;
using TrialApplication.Interfaces;
using TrialApplication.Models.DTO;
using TrialApplication.Models;

namespace TrialApplication.Services
{
    public class EventService : IEventService
    {

        private readonly IRepository<int, Event> _eventRepo;
        private readonly IMapper _mapper;

        public EventService(IRepository<int, Event> eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;

        }

        public async Task<int> CreateEvent(EventDTO eventDTO)
        {

            Event newEvent  = _mapper.Map<Event>(eventDTO);
            var addedEvent= await _eventRepo.Add(newEvent);
            return addedEvent.EventId;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            try
            {
                var emp = await _eventRepo.GetAll();
                return emp;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Employees");
            }
        }
    }
}

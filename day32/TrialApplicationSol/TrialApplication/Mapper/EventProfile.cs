
using AutoMapper;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Mapper
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDTO, Event>();
            CreateMap<Event, EventDTO>();
        }
    }
}


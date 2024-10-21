

using AutoMapper;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Mapper
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDTO>();
        }
    }
}


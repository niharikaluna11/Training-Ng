using AutoMapper;
using ComplaintTicketAPI.Models;
using ComplaintTicketAPI.Models.DTO;

namespace ComplaintTicketAPI.Mapper
{
    public class Userprofilemapper: Profile
    {
        public Userprofilemapper()
        {
            CreateMap<User, UserProfile>();  // Map from ProfileUpdateDTO to Profile
            CreateMap<User, UserDTO>()
           .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
        }


    }
}

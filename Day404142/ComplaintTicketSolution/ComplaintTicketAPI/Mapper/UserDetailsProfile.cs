using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using AutoMapper;
using Profile = AutoMapper.Profile;

namespace ComplaintTicketAPI.Mapper
{
    public class UserDetailsProfile : Profile
    {
        public UserDetailsProfile()
            {
                // Mapping from Complaint to ComplaintDTO
                CreateMap<User, UserDTO>().ReverseMap();
            }
        
    }
}

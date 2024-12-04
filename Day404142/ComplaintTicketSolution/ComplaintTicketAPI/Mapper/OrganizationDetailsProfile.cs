using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using Profile = AutoMapper.Profile;

namespace ComplaintTicketAPI.Mapper
{
    public class OrganizationDetailsProfile : Profile
    {
        public OrganizationDetailsProfile()
        {
            // Mapping from Complaint to ComplaintDTO
            CreateMap<Organization, OrganizationDTO>().ReverseMap();

            CreateMap<Organization, OrganizationIdDTO>()
                .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.orgId)); ;
        }
    }
}

using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

public class ComplaintStatusProfile : Profile
{
    public ComplaintStatusProfile()
    {
        CreateMap<ComplaintStatus, ComplaintStatusDTO>()
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StatusDate, opt => opt.MapFrom<StatusDateResolver>())  // Use the custom resolver here
            .ForMember(dest => dest.CommentByUser, opt => opt.MapFrom(src => src.CommentByUser));
    }
}

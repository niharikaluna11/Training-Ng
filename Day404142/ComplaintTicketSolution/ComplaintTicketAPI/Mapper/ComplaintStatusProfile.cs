using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;
using AutoMapper;

namespace ComplaintTicketAPI.Mapper
{
    public class ComplaintStatusProfile : Profile
    {
        public ComplaintStatusProfile()
        {
            // Map from ComplaintStatusDTO to ComplaintStatus
            CreateMap<ComplaintStatusDTO, ComplaintStatus>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.CommentByUser, opt => opt.MapFrom(src => src.CommentByUser))
                .ForMember(dest => dest.ComplaintStatusDates, opt => opt.MapFrom(src=>src.StatusDate)); // Ignoring ComplaintStatusDates as it might be handled separately

            // Map from ComplaintStatusDTO to ComplaintStatusDate (for status date)
            CreateMap<ComplaintStatusDTO, ComplaintStatusDate>()
                .ForMember(dest => dest.StatusDate, opt => opt.MapFrom(src => src.StatusDate));
        }

        //CreateMap<ComplaintStatus, ComplaintStatusDTO>()
        //    .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
        //    .ForMember(dest => dest.StatusDate, opt => opt.MapFrom(src => src.StatusDate))
        //    // Add other necessary mappings
        //    ;

    }
}
using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Mapper
{
    public class UpdateComplaintProfile : Profile
    {
        public UpdateComplaintProfile()
        {
            // Map UpdateComplaintRequestDTO to Complaint
            CreateMap<UpdateComplaintRequestDTO, Complaint>()
                .ForMember(dest => dest.ComplaintId, opt => opt.MapFrom(src => src.ComplaintId))
                // Note: Mapping only necessary fields here
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) // Ignored if no update in category
                .ForMember(dest => dest.ComplaintStatusDates, opt => opt.Ignore()) // Ignored for now
                .ForMember(dest => dest.ComplaintFiles, opt => opt.Ignore()); // Ignored for now

            // Map UpdateComplaintRequestDTO to ComplaintStatus
            CreateMap<UpdateComplaintRequestDTO, ComplaintStatus>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CommentByUser, opt => opt.MapFrom(src => src.CommentByUser))
                .ForMember(dest => dest.Priority, opt => opt.Ignore()) // Ignored if priority is not updated
                .ForMember(dest => dest.ComplaintStatusDates, opt => opt.Ignore()); // Ignored for now
        }
    }
}

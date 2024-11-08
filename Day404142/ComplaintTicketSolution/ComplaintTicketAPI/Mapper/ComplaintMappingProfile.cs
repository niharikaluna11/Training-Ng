using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

public class ComplaintMappingProfile : Profile
{
    public ComplaintMappingProfile()
    {
        // Map CreateComplaintRequestDTO to Complaint
        CreateMap<CreateComplaintRequestDTO, Complaint>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.OrganizationId, opt => opt.MapFrom(src => src.OrganizationId))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

        // Map CreateComplaintRequestDTO to ComplaintStatus (Assuming the status comes from the DTO)
        CreateMap<CreateComplaintRequestDTO, ComplaintStatus>()
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status)) // Ensure correct casing
            .ForMember(dest => dest.CommentByUser, opt => opt.MapFrom(src => src.CommentByUser));

        // Map CreateComplaintRequestDTO to ComplaintFile (FilePath mapping)
        CreateMap<CreateComplaintRequestDTO, ComplaintFile>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.AttachmentUrl));


    }
}

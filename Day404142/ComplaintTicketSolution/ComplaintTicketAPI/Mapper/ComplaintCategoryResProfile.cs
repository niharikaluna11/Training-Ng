using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Mapper
{
    public class ComplaintCategoryResProfile : Profile
    {
           public ComplaintCategoryResProfile()
            {
                // Map ComplaintCategory to ComplaintCategoryResponseDTO for output
                CreateMap<ComplaintCategory, ComplaintCategoryResponseDTO>();

                // Map ComplaintCategoryDTO to ComplaintCategory for input
                CreateMap<ComplaintCategoryDTO, ComplaintCategory>();
            }
        }
    }


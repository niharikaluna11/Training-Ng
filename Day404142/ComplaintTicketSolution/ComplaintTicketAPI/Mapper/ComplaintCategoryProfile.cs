using AutoMapper;
using ComplaintTicketAPI.Models.DTO;
using ComplaintTicketAPI.Models;

namespace ComplaintTicketAPI.Mapper
{
    public class ComplaintCategoryProfile : Profile
    {
        public ComplaintCategoryProfile() {

            CreateMap<ComplaintCategory, ComplaintCategoryDTO>().ReverseMap();
        }
    }
}

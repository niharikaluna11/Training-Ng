using AutoMapper;
using TrialApplication.Models;
using TrialApplication.Models.DTO;

namespace TrialApplication.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}

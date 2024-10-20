using AutoMapper;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;

namespace EFFirstAPI.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}

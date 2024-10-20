using AutoMapper;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;

namespace EFFirstAPI.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}

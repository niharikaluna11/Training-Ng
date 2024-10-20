using AutoMapper;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;

namespace EFFirstAPI.Mappers
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>();
            CreateMap<ProductImageDTO, ProductImage>();

        }
    }
}

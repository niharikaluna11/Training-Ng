using AutoMapper;
using EFFirstAPI.Interfaces;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Models;

namespace EFFirstAPI.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<int, ProductImage> _productImageRepo;
        private readonly IMapper _mapper;

        public ProductImageService(IRepository<int, ProductImage> productImageRepository, IMapper mapper)
        {
            _productImageRepo = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ProductImage> CreateProductImage(ProductImageDTO productImage)
        {
            ProductImage newProductImage = _mapper.Map<ProductImage>(productImage);
            var addedProductImage = await _productImageRepo.Add(newProductImage);
            return addedProductImage;
        }
    }
}

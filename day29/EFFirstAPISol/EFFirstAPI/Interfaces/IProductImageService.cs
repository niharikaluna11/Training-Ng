using EFFirstAPI.Models;
using EFFirstAPI.Models.DTO;

namespace EFFirstAPI.Interfaces
{
    public interface IProductImageService
    {
        Task<ProductImage> CreateProductImage(ProductImageDTO product);
    }
}

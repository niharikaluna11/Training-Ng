using EFFirstAPI.Models;
using EFFirstAPI.Models.DTO;

namespace EFFirstAPI.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateProduct(ProductDTO product);

        Task<Product> UpdateProductPrice(float price, int Id);

        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProduct(int Id);
    }
}

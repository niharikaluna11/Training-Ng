using EFFirstAPI.Interfaces;
using EFFirstAPI.Models;
using EFFirstAPI.Exceptions;
using EFFirstAPI.Models.DTO;
using EFFirstAPI.Mappers;
using AutoMapper;


namespace EFFirstAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<int, Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IRepository<int, Product> productRepository, IMapper mapper)
        {
            _productRepo = productRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateProduct(ProductDTO product)
        {
            Product newProduct = _mapper.Map<Product>(product);
            var addProduct = await _productRepo.Add(newProduct);
            return addProduct.Id;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            try
            {
                var products = await _productRepo.GetAll();
                return products;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("Product");
            }
        }

        public async Task<Product> GetProduct(int Id)
        {
            try
            {
                var product = await _productRepo.Get(Id);
                if (product != null)
                {
                    return product;
                }
                throw new NotFoundException("Product");
            }
            catch (Exception)
            {
                throw new NotFoundException("Product");
            }
        }

        public async Task<Product> UpdateProductPrice(float price, int Id)
        {
            var oldProduct = await _productRepo.Get(Id);
            if (oldProduct != null)
            {
                oldProduct.Price = price;
                return oldProduct;
            }
            throw new NotUpdateException("Product Price Update Fail");
        }
     // Task<Product> UpdateProductPrice(float price, int Id);
    }
}

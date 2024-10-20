using EFFirstAPI.Context;
using EFFirstAPI.Exceptions;
using EFFirstAPI.Interfaces;
using EFFirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFFirstAPI.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ShoppingContext _context;

        public ProductRepository(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception)
            {
                throw new CouldNotAddException("Product");
            }
        }

      

        public async Task<Product> Delete(int key)
        {
            var product = await Get(key);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
            throw new NotFoundException("Product for delete");
        }

        public async Task<Product> Get(int key)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == key);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return products;
        }
    }
}

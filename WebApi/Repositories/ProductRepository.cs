
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Product?> GetProductAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetPaginatedProductsAsync(int page, int pageSize)
        {
            return await _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<bool> UpdateProductDescriptionAsync(int productId, string? description)
        {
            var product = await GetProductAsync(productId);

            try
            {
                product.Description = description;
                return await SaveChangesAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }
        public async Task<bool> ProductExists(int productId)
        {
            return (await GetProductAsync(productId)) != null;
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}
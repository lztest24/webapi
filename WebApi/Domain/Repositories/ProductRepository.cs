
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Entities;
using WebApi.Extensions;

namespace WebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        private readonly ILogger _logger;

        public ProductRepository(ProductContext context, ILogger<ProductRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }
        public async Task<Product?> GetProductAsync(int productId, CancellationToken token)
        {
            return await _context.Products.FindAsync(productId, token);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token)
        {
            return await _context.Products.ToListAsync(token);

        }
        public async Task<IEnumerable<Product>> GetPaginatedProductsAsync(int page, int pageSize, CancellationToken token)
        {
            return await _context.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<bool> UpdateProductDescriptionAsync(int productId, string? description, CancellationToken token)
        {
            await _context.Database.BeginTransactionAsync(token);

            var product = await GetProductAsync(productId, token);
            product!.Description = description;

            var result = await SaveChangesAsync(token);
            await _context.Database.CommitTransactionAsync(token);
            return result;
        }

        public async Task<int> GetProductCountAsync(CancellationToken token)
        {
            return await _context.Products.CountAsync();
        }
        public async Task<bool> ProductExists(int productId, CancellationToken token)
        {
            return (await GetProductAsync(productId, token)) != null;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token) >= 0;
        }

    }
}
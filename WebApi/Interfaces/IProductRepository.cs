
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token);
        Task<IEnumerable<Product>> GetPaginatedProductsAsync(int page, int pageSize, CancellationToken token);
        Task<Product?> GetProductAsync(int productId, CancellationToken token);
        Task<bool> UpdateProductDescriptionAsync(int productId, string description, CancellationToken token);
        Task<int> GetProductCountAsync(CancellationToken token);
        Task<bool> ProductExists(int productId, CancellationToken token);
        Task<bool> SaveChangesAsync(CancellationToken token);
    }
}

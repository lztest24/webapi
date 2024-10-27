
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetPaginatedProductsAsync(int page, int pageSize);
        Task<Product?> GetProductAsync(int productId);
        Task<bool> UpdateProductDescriptionAsync(int productId, string description);
        Task<int> GetProductCountAsync();
        Task<bool> ProductExists(int productId);
        Task<bool> SaveChangesAsync();
    }
}

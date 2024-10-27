
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductPaginationDto> GetProductsAsync(int page, int pageSize = 10);
        Task<ProductDto?> GetProductAsync(int productId);
        Task<bool> UpdateProductDescriptionAsync(int productId, string description);
        Task<bool> ProductExists(int productId);
    }
}

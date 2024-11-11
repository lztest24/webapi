
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IProductService
    {
        Task<ProductsDto> GetProductsAsync(CancellationToken token);
        Task<ProductPaginationDto> GetProductsAsync(int page, int pageSize, CancellationToken token);
        Task<ProductDto?> GetProductAsync(int productId, CancellationToken token);
        Task<bool> UpdateProductDescriptionAsync(int productId, string description, CancellationToken token);
        Task<bool> ProductExists(int productId, CancellationToken token);
    }
}

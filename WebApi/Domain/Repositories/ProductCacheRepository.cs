
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Entities;
using WebApi.Extensions;
using WebApi.Utility;
using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Repositories
{
    public class ProductCacheRepository : IProductRepository
    {
        private readonly IProductRepository _repo;
        private readonly IMemoryCache _cache;

        public ProductCacheRepository(IProductRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetPaginatedProductsAsync(int page, int pageSize, CancellationToken token)
        {
            var entry = _cache.GetOrCreateAsync(CacheKey.Get(CacheKey.Prefix.PRODUCTPAGE, page, pageSize), entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _repo.GetPaginatedProductsAsync(page, pageSize, token);
            });

            return await entry;

        }

        public async Task<Product?> GetProductAsync(int productId, CancellationToken token)
        {
            var entry = _cache.GetOrCreateAsync(CacheKey.Get(CacheKey.Prefix.PRODUCT, productId), entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _repo.GetProductAsync(productId, token);
            });

            return await entry;
        }

        public async Task<int> GetProductCountAsync(CancellationToken token)
        {
            return await _repo.GetProductCountAsync(token);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token)
        {
            return await _repo.GetProductsAsync(token);
        }

        public async Task<bool> ProductExists(int productId, CancellationToken token)
        {
            return await _repo.ProductExists(productId, token);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken token)
        {
            return await _repo.SaveChangesAsync(token);
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description, CancellationToken token)
        {
            var result = await _repo.UpdateProductDescriptionAsync(productId, description, token);
            if (result)
            {
                _cache.Remove(CacheKey.Get(CacheKey.Prefix.PRODUCT));
                foreach (var key in (_cache as MemoryCache)!.Keys)
                {
                    if ($"{key}".StartsWith($"{CacheKey.Prefix.PRODUCTPAGE}_"))
                    {
                        _cache.Remove(key);
                    }
                }
            }
            return result;
        }
    }
}
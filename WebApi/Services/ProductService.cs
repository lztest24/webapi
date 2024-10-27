
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Utility;

namespace WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto?> GetProductAsync(int productId)
        {
            return _mapper.Map<ProductDto>(await _repo.GetProductAsync(productId));
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetProductsAsync());
        }
        public async Task<ProductPaginationDto> GetProductsAsync(int page, int pageSize = 10)
        {
            var result = new ProductPaginationDto { };
            result.Products = _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetPaginatedProductsAsync(page, pageSize));

            result.PaginationMetadata.CurrentPage = page;
            result.PaginationMetadata.PageSize = pageSize;
            result.PaginationMetadata.TotalRecords = await _repo.GetProductCountAsync();
            result.PaginationMetadata.TotalPages = (int)Math.Ceiling(result.PaginationMetadata.TotalRecords.SafeDiv(pageSize));
            result.PaginationMetadata.PreviousPage = page <= 1 ? null : page - 1;
            result.PaginationMetadata.NextPage = result.PaginationMetadata.TotalPages <= page ? null : page + 1;
            
            return result;
        }

        public async Task<bool> ProductExists(int productId)
        {
            return await _repo.ProductExists(productId);
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description)
        {
            return await _repo.UpdateProductDescriptionAsync(productId, description);
        }
    }
}
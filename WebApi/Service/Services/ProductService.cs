
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Extensions;

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

        public async Task<ProductDto?> GetProductAsync(int productId, CancellationToken token)
        {
            return _mapper.Map<ProductDto>(await _repo.GetProductAsync(productId, token));
        }

        public async Task<ProductsDto> GetProductsAsync(CancellationToken token)
        {
            var products = _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetProductsAsync(token));
            return new ProductsDto {Products = products };
        }
        public async Task<ProductPaginationDto> GetProductsAsync(int page, int pageSize, CancellationToken token)
        {
            var result = new ProductPaginationDto { };
            result.Products = _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetPaginatedProductsAsync(page, pageSize, token));

            result.PaginationMetadata.CurrentPage = page;
            result.PaginationMetadata.PageSize = pageSize;
            result.PaginationMetadata.TotalRecords = await _repo.GetProductCountAsync(token);
            result.PaginationMetadata.TotalPages = (int)Math.Ceiling(result.PaginationMetadata.TotalRecords.SafeDiv(pageSize));
            result.PaginationMetadata.PreviousPage = page <= 1 ? null : page - 1;
            result.PaginationMetadata.NextPage = result.PaginationMetadata.TotalPages <= page ? null : page + 1;

            return result;
        }

        public async Task<bool> ProductExists(int productId, CancellationToken token)
        {
            return await _repo.ProductExists(productId, token);
        }

        public async Task<bool> UpdateProductDescriptionAsync(int productId, string description, CancellationToken token)
        {
            return await _repo.UpdateProductDescriptionAsync(productId, description, token);
        }
    }
}
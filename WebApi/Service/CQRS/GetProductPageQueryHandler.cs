using MediatR;
using WebApi.Interfaces;
using WebApi.Models;

public class GetProductPageQueryHandler(IProductService service) : IRequestHandler<GetProductPageQuery, ProductPaginationDto>
{
    public Task<ProductPaginationDto> Handle(GetProductPageQuery query, CancellationToken token)
    {
        return service.GetProductsAsync(query.Page, query.PageSize, token);
    }
}
using MediatR;
using WebApi.Interfaces;
using WebApi.Models;

public class GetProductsQueryHandler(IProductService service) : IRequestHandler<GetProductsQuery, ProductsDto>
{
    public Task<ProductsDto> Handle(GetProductsQuery query, CancellationToken token)
    {
        return service.GetProductsAsync(token);
    }
}
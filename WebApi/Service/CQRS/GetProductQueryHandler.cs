using MediatR;
using WebApi.Interfaces;
using WebApi.Models;

public class GetProductQueryHandler(IProductService service) : IRequestHandler<GetProductQuery, ProductDto?>
{
    public Task<ProductDto?> Handle(GetProductQuery query, CancellationToken token)
    {
        return service.GetProductAsync(query.Id, token);
    }
}
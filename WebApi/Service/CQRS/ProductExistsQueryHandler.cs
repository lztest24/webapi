using MediatR;
using WebApi.Interfaces;
using WebApi.Models;

public class ProductExistsQueryHandler(IProductService service) : IRequestHandler<ProductExistsQuery, bool>
{
    public Task<bool> Handle(ProductExistsQuery query, CancellationToken token)
    {
        return service.ProductExists(query.Id, token);
    }
}
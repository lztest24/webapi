using MediatR;
using WebApi.Models;

public class GetProductQuery(int id) : IRequest<ProductDto?>
{
    public int Id { get; set; } = id;
}
using MediatR;
using WebApi.Models;

public class ProductExistsQuery(int id) : IRequest<bool>
{
    public int Id { get; set; } = id;
}
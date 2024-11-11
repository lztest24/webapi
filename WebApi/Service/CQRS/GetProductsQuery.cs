using MediatR;
using WebApi.Models;

public class GetProductsQuery() : IRequest<ProductsDto?>;
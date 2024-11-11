using MediatR;
using WebApi.Models;

public class GetProductPageQuery(int page, int pageSize) : IRequest<ProductPaginationDto?>
{
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
}
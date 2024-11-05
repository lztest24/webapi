namespace WebApi.Models;

public class ProductPaginationDto
{
    public IEnumerable<ProductDto> Products { get; set; }
    public PaginationMetadataDto PaginationMetadata { get; set; } = new PaginationMetadataDto {};
}


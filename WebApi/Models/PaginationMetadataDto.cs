using System.Text.Json.Serialization;

namespace WebApi.Models;

public class PaginationMetadataDto
{
    [JsonPropertyName("totalItems")]
    public int TotalRecords { get; set; }
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
    [JsonPropertyName("pageCount")]
    public int TotalPages { get; set; }
    [JsonPropertyName("prevPage")]
    public int? PreviousPage { get; set; }
    [JsonPropertyName("nextPage")]
    public int? NextPage { get; set; }
}


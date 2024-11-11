using MediatR;
using WebApi.Models;

public class UpdateProductDescriptionCommand(int id, string description) : IRequest<bool>
{
    public int Id { get; set; } = id;
    public string Description { get; set; } = description;
}
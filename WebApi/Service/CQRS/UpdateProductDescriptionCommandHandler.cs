using MediatR;
using WebApi.Interfaces;
using WebApi.Models;

public class UpdateProductDescriptionCommandHandler(IProductService service) : IRequestHandler<UpdateProductDescriptionCommand, bool>
{
    public Task<bool> Handle(UpdateProductDescriptionCommand command, CancellationToken token)
    {
        return service.UpdateProductDescriptionAsync(command.Id, command.Description, token);
    }
}
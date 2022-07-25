using PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Create;

namespace PhoneRegistryDDD.Warehouse.Api.Requests;

public record CreateSimCardRequest(string Number, string Pin, string Puk)
{
    public CreateSimCard AsCommand() => new(Number, Pin, Puk);
}
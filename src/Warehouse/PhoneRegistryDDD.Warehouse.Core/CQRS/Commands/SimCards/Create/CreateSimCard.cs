using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Create;

public record CreateSimCard(string Number, string Pin, string Puk) : CreateCommand;
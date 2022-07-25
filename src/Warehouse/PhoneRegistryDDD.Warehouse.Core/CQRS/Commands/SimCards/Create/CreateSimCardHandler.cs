using System;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Exceptions;
using PhoneRegistryDDD.Warehouse.Core.DAL.Repositories.Abstracts;
using PhoneRegistryDDD.Warehouse.Core.Entities;
using PhoneRegistryDDD.Warehouse.Core.Events;
using SuligaPawel.Common.CQRS.Commands;
using SuligaPawel.Common.CQRS.Events.Dispatchers.Sync;

namespace PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Create;

public class CreateSimCardHandler : ICommandHandler<CreateSimCard>
{
    private readonly ISimCardRepository _repo;
    private readonly IEventDispatcher _eventDispatcher;

    public CreateSimCardHandler(ISimCardRepository repo, IEventDispatcher eventDispatcher)
    {
        _repo = repo;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(CreateSimCard command, CancellationToken cancellationToken)
    {
        await ValidNumber(command.Number);
        ValidPin(command.Pin);
        ValidPuk(command.Puk);

        var newSimCard = new SimCard
        {
            Id = Guid.NewGuid(),
            Number = command.Number,
            Pin = command.Pin,
            Puk = command.Puk
        };

        await _repo.Add(newSimCard);
        await _eventDispatcher.Publish(new SimCardCreated(newSimCard.Id));
    }

    private async Task ValidNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length != 9)
        {
            throw new PhoneNumberDoesNotValidException(number, "the number of digits is incorrect.");
        }

        if (await NumberRegistered(number))
        {
            throw new PhoneNumberDoesNotValidException(number, "is already registered.");
        }
    }

    private Task<bool> NumberRegistered(string number)
    {
        return _repo.ExistsAlready(number);
    }

    private void ValidPin(string pin)
    {
        if (string.IsNullOrWhiteSpace(pin) || pin.Length != 4)
        {
            throw new PinDoesNotValidException(pin, "the number of digits is incorrect.");
        }
    }

    private void ValidPuk(string puk)
    {
        if (string.IsNullOrWhiteSpace(puk) || puk.Length is < 8 or > 12)
        {
            throw new PukDoesNotValidException(puk, "the number of digits is incorrect.");
        }
    }
}
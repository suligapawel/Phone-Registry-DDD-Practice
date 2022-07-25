using System;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using SuligaPawel.Common.CQRS.Events;

namespace PhoneRegistryDDD.Availability.Application.Events.SimCards;

public class AssortmentCreatedHandler : IEventHandler<SimCardCreated>
{
    private readonly IAssortmentRepository _assortments;

    public AssortmentCreatedHandler(IAssortmentRepository assortments)
    {
        _assortments = assortments;
    }

    public async Task Handle(SimCardCreated @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        await _assortments.Add(Assortment.New(@event.Id));
    }
}
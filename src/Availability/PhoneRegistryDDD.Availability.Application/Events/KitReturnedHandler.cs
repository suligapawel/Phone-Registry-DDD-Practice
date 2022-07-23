using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Repositories;
using SuligaPawel.Common.CQRS.Events;
using SuligaPawel.Common.CQRS.Events.Dispatchers.Sync;

namespace PhoneRegistryDDD.Availability.Application.Events;

public class KitReturnedHandler : IEventHandler<KitReturned>
{
    private readonly IAssortmentRepository _assortmentRepo;
    private readonly IEventDispatcher _eventDispatcher;

    public KitReturnedHandler(
        IAssortmentRepository assortmentRepo,
        IEventDispatcher eventDispatcher)
    {
        _assortmentRepo = assortmentRepo;
        _eventDispatcher = eventDispatcher;
    }

    public async Task Handle(KitReturned @event, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(@event);

        var unblockedAssortments = new List<bool>();
        var assortments = (await _assortmentRepo.GetFewBy(new[] { @event.DeviceId, @event.SimCardId })).ToList();

        foreach (var assortment in assortments)
        {
            unblockedAssortments.Add(assortment.Unblock());
        }

        var unblockedAllAssortments = unblockedAssortments.Count.Equals(assortments.Count);

        if (!unblockedAllAssortments)
        {
            return;
        }

        await _assortmentRepo.UpdateFew(assortments);
        await _eventDispatcher.Publish(new AssortmentUnblocked(assortments.Select(x => x.Id).ToArray()));
    }
}
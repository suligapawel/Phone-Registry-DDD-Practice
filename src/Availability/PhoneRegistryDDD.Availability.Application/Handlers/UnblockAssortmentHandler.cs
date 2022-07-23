using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using SuligaPawel.Common.CQRS.Commands;
using SuligaPawel.Common.CQRS.Events.Dispatchers.Sync;

namespace PhoneRegistryDDD.Availability.Application.Handlers;

public class UnblockAssortmentHandler : ICommandHandler<UnblockAssortmentCommand>
{
    private readonly IAssortmentRepository _assortmentRepo;
    private readonly IEventDispatcher _eventsDispatcher;

    public UnblockAssortmentHandler(IAssortmentRepository assortmentRepository, IEventDispatcher eventsDispatcher)
    {
        _assortmentRepo = assortmentRepository;
        _eventsDispatcher = eventsDispatcher;
    }

    public async Task Handle(UnblockAssortmentCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var unblockedAssortments = new List<bool>();
        IEnumerable<Assortment> assortments = (await _assortmentRepo.GetFewBy(command.Ids)).ToList();

        foreach (var assortment in assortments)
        {
            unblockedAssortments.Add(assortment.Unblock());
        }

        var unblockedAllAssortments = unblockedAssortments.Count.Equals(assortments.Count());

        if (!unblockedAllAssortments)
        {
            return;
        }

        await _assortmentRepo.UpdateFew(assortments);
        await _eventsDispatcher.Publish(new AssortmentUnblocked(command.Ids.ToArray()));
    }
}
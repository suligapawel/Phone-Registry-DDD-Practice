using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Application.Events;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Orchestrating.Abstractions.Kit;
using SuligaPawel.Common.CQRS.CancellationTokens;
using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Orchestrating.Kit;

public class TakeBackKitService : ITakeBackKitFacade
{
    private readonly ICommandHandler<TakeBackKitCommand> _takeBackKitHandler;
    private readonly ICommandHandler<UnblockAssortmentCommand> _unlockAssortmentHandler;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public TakeBackKitService(
        ICommandHandler<TakeBackKitCommand> takeBackKitHandler,
        ICommandHandler<UnblockAssortmentCommand> unlockAssortmentHandler,
        ICancellationTokenProvider cancellationTokenProvider)
    {
        _takeBackKitHandler = takeBackKitHandler;
        _unlockAssortmentHandler = unlockAssortmentHandler;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    // TODO: Transaction
    public async Task TakeBack(TakeBackKitCommand takeBackKitCommand)
    {
        await _takeBackKitHandler.Handle(takeBackKitCommand, _cancellationTokenProvider.CreateToken());

        // if (kitReturned == null)
        // {
        //     return false;
        // }
        //
        // var assortmentUnblocked = await _unlockAssortmentHandler.Handle(MapToCommand(kitReturned));
        //
        // return assortmentUnblocked != null;
    }

    private static UnblockAssortmentCommand MapToCommand(KitReturned @event) => new(new[] { @event.DeviceId, @event.SimCardId });
}
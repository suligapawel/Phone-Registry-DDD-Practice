using System;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Helpdesk.Application.Events;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Orchestrating.Abstractions.Kit;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Orchestrating.Kit
{
    public class TakeBackKitService : ITakeBackKitFacade
    {
        private readonly ICommandHandler<TakeBackKitCommand, KitReturned> _takeBackKitHandler;
        private readonly ICommandHandler<UnblockAssortmentCommand, AssortmentUnblocked> _unlockAssortmentHandler;

        public TakeBackKitService(ICommandHandler<TakeBackKitCommand, KitReturned> takeBackKitHandler,
            ICommandHandler<UnblockAssortmentCommand, AssortmentUnblocked> unlockAssortmentHandler)
        {
            _takeBackKitHandler = takeBackKitHandler;
            _unlockAssortmentHandler = unlockAssortmentHandler;
        }

        public async Task<bool> TakeBack(TakeBackKitCommand takeBackKitCommand)
        {
            //TODO: Transaction
            KitReturned kitReturned = await _takeBackKitHandler.Handle(takeBackKitCommand);

            if (kitReturned == null)
                return false;

            AssortmentUnblocked assortmentUnblocked = await _unlockAssortmentHandler.Handle(MapToCommand(kitReturned));

            return assortmentUnblocked != null;
        }

        private static UnblockAssortmentCommand MapToCommand(KitReturned @event)
            => new UnblockAssortmentCommand(new Guid[] {@event.DeviceId, @event.SimCardId});
    }
}
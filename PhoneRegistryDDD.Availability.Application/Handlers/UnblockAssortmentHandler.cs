using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Availability.Application.Handlers
{
    public class UnblockAssortmentHandler : ICommandHandler<UnblockAssortmentCommand>
    {
        private readonly IAssortmentRepository _assortmentRepo;

        public UnblockAssortmentHandler(IAssortmentRepository assortmentRepository)
        {
            _assortmentRepo = assortmentRepository;
        }

        public async Task Handle(UnblockAssortmentCommand notification)
        {
            IEnumerable<Assortment> assortments = await _assortmentRepo.GetFewBy(notification.Ids);

            foreach (var assortment in assortments)
            {
                assortment.Unblock();
            }

            await _assortmentRepo.UpdateFew(assortments);
        }
    }
}

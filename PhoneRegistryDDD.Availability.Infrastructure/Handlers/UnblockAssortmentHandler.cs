using MediatR;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.Availability.Infrastructure.Handlers
{
    public class UnblockAssortmentHandler : INotificationHandler<UnblockAssortmentCommand>
    {
        private readonly IAssortmentRepository _assortmentRepo;

        public UnblockAssortmentHandler(IAssortmentRepository assortmentRepository)
        {
            _assortmentRepo = assortmentRepository;
        }

        public async Task Handle(UnblockAssortmentCommand notification, CancellationToken cancellationToken)
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

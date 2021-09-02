using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneRegistryDDD.Availability.Application.Events;
using PhoneRegistryDDD.Availability.Core.Commands;
using PhoneRegistryDDD.Availability.Core.Entities;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Availability.Application.Handlers
{
    public class UnblockAssortmentHandler : ICommandHandler<UnblockAssortmentCommand, AssortmentUnblocked>
    {
        private readonly IAssortmentRepository _assortmentRepo;

        public UnblockAssortmentHandler(IAssortmentRepository assortmentRepository)
        {
            _assortmentRepo = assortmentRepository;
        }

        public async Task<AssortmentUnblocked> Handle(UnblockAssortmentCommand command)
        {
            List<bool> unblockedAssortments = new List<bool>();
            IEnumerable<Assortment> assortments = (await _assortmentRepo.GetFewBy(command.Ids)).ToList();

            foreach (var assortment in assortments)
            {
                unblockedAssortments.Add(assortment.Unblock());
            }

            bool unblockedAllAssortments = unblockedAssortments.Count.Equals(assortments.Count());

            if (!unblockedAllAssortments) return null;

            //TODO: remove null
            bool updateResult = await _assortmentRepo.UpdateFew(assortments);
            return updateResult
                ? new AssortmentUnblocked(command.Ids.ToArray())
                : null;
        }
    }
}
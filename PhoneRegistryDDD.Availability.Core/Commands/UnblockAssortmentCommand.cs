using MediatR;
using System;
using System.Collections.Generic;

namespace PhoneRegistryDDD.Availability.Core.Commands
{
    public class UnblockAssortmentCommand : INotification
    {
        public IEnumerable<Guid> Ids { get; }

        public UnblockAssortmentCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}

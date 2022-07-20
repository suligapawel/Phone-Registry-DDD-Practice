using System;
using System.Collections.Generic;
using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Availability.Core.Commands;

public class UnblockAssortmentCommand : ICommand
{
    public IEnumerable<Guid> Ids { get; }

    public UnblockAssortmentCommand(IEnumerable<Guid> ids)
    {
        Ids = ids;
    }
}
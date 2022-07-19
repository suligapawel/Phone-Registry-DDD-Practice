using System;
using System.Collections.Generic;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Availability.Core.Commands;

public class UnblockAssortmentCommand : ICommand
{
    public IEnumerable<Guid> Ids { get; }

    public UnblockAssortmentCommand(IEnumerable<Guid> ids)
    {
        Ids = ids;
    }
}
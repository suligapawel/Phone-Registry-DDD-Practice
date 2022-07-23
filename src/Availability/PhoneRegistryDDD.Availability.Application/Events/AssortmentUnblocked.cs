using System;
using System.Collections.Generic;
using SuligaPawel.Common.CQRS.Events;

namespace PhoneRegistryDDD.Availability.Application.Events;

public record AssortmentUnblocked(IReadOnlyCollection<Guid> AssortmentIds) : IEvent;
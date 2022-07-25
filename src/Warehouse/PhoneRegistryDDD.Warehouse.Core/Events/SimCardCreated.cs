using System;
using SuligaPawel.Common.CQRS.Events;

namespace PhoneRegistryDDD.Warehouse.Core.Events;

public record SimCardCreated(Guid Id) : IEvent;
using System;
using SuligaPawel.Common.CQRS.Events;

namespace PhoneRegistryDDD.Availability.Application.Events;

public record KitReturned(Guid DeviceId, Guid SimCardId) : IEvent;
using System;
using SuligaPawel.Common.CQRS.Events;

namespace PhoneRegistryDDD.Availability.Application.Events.SimCards;

public record SimCardCreated(Guid Id) : IEvent;
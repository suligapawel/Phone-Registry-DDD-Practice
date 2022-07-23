using System;
using SuligaPawel.Common.CQRS.Commands;

namespace PhoneRegistryDDD.Helpdesk.Core.Commands;

public record TakeBackKitCommand(Guid EmployeeId, Guid DeviceId) : ICommand;
using Microsoft.Extensions.Logging;
using SuligaPawel.Common.Exceptions.Exceptions;

namespace PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Exceptions;

public class PinDoesNotValidException : AppException
{
    public override LogLevel LogLevel => LogLevel.Information;

    public PinDoesNotValidException(string pin, string details)
        : base($"{pin} - {details}")
    {
    }
}
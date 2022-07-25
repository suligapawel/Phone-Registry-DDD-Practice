using Microsoft.Extensions.Logging;
using SuligaPawel.Common.Exceptions.Exceptions;

namespace PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Exceptions;

public class PhoneNumberDoesNotValidException : AppException
{
    public override LogLevel LogLevel => LogLevel.Information;

    public PhoneNumberDoesNotValidException(string simCardNumber, string details)
        : base($"{simCardNumber} - {details}")
    {
    }
}
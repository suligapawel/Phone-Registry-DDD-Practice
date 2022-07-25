using Microsoft.Extensions.Logging;
using SuligaPawel.Common.Exceptions.Exceptions;

namespace PhoneRegistryDDD.Warehouse.Core.CQRS.Commands.SimCards.Exceptions;

public class PukDoesNotValidException : AppException
{
    public override LogLevel LogLevel => LogLevel.Information;

    public PukDoesNotValidException(string puk, string details)
        : base($"{puk} - {details}")
    {
    }
}
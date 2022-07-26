using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Warehouse.Api.Requests;
using SuligaPawel.Common.CQRS.Commands.Dispatchers;

namespace PhoneRegistryDDD.Warehouse.Api.Controllers;

[ApiController]
[Route("warehouse/[controller]")]
public class SimCardsController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public SimCardsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSimCardRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        await _commandDispatcher.Execute(request.AsCommand());

        return Ok();
    }
}
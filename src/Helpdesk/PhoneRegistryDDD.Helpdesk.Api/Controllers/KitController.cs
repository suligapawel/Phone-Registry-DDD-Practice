using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using SuligaPawel.Common.CQRS.Commands.Dispatchers;

namespace PhoneRegistryDDD.Helpdesk.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KitController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public KitController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _commandDispatcher.Execute(
            new TakeBackKitCommand(
                Guid.Parse("0e28ce37-7833-42f8-b1f6-6b83e7e7fec8"),
                Guid.Parse("bd1b49a8-4a0d-4600-b99b-c649f6ca5404")));

        return await Task.FromResult(Ok());
    }

    [HttpPost]
    public async Task<IActionResult> Post(TakeBackKitCommand command)
    {
        await _commandDispatcher.Execute(command);
        return Ok();
    }
}
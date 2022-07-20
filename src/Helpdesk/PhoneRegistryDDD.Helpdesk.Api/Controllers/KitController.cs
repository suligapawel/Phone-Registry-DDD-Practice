using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Orchestrating.Abstractions.Kit;
using SuligaPawel.Common.CQRS.Commands.Dispatchers;

namespace PhoneRegistryDDD.Helpdesk.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class KitController : ControllerBase
{
    private readonly ITakeBackKitFacade _takeBackKitFacade;
    private readonly ICommandDispatcher _commandDispatcher;

    public KitController(ITakeBackKitFacade takeBackKitFacade, ICommandDispatcher commandDispatcher)
    {
        _takeBackKitFacade = takeBackKitFacade;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        await _commandDispatcher.Execute(new TakeBackKitCommand());
        await _takeBackKitFacade.TakeBack(new TakeBackKitCommand());

        return await Task.FromResult(Ok());
    }

    [HttpPost]
    public async Task<IActionResult> Post(TakeBackKitCommand command)
    {
        await _takeBackKitFacade.TakeBack(command);
        return Ok();
    }
}
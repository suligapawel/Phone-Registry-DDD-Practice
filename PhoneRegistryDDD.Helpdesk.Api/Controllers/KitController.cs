using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Shared.Abstractions.Commands;

namespace PhoneRegistryDDD.Helpdesk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KitController : ControllerBase
    {
        //TODO: Change to facade
        private readonly ICommandHandler<TakeBackKitCommand, bool> _handler;

        public KitController(ICommandHandler<TakeBackKitCommand, bool> handler)
        {
            _handler = handler;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(TakeBackKitCommand command)
        {
            var result = await _handler.Handle(command);
            return Ok(result);
        }
    }
}
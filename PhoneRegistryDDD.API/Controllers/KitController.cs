using MediatR;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using System.Threading.Tasks;

namespace PhoneRegistryDDD.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TakeBackKitCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

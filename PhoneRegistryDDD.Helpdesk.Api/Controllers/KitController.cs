using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneRegistryDDD.Helpdesk.Core.Commands;
using PhoneRegistryDDD.Orchestrating.Abstractions.Kit;

namespace PhoneRegistryDDD.Helpdesk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KitController : ControllerBase
    {
        private readonly ITakeBackKitFacade _takeBackKitFacade;

        public KitController(ITakeBackKitFacade takeBackKitFacade)
        {
            _takeBackKitFacade = takeBackKitFacade;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(TakeBackKitCommand command)
        {
            var result = await _takeBackKitFacade.TakeBack(command);
            return Ok(result);
        }
    }
}
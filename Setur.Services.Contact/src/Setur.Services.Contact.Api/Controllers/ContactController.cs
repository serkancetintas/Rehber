using Microsoft.AspNetCore.Mvc;
using Setur.Services.Contact.Api.Controllers.Base;
using Setur.Services.Contact.Application.Commands;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController: BaseApiController
    {
        private readonly ICommandHandler<CreateContact> _commandHandler;

        public ContactController(ICommandHandler<CreateContact> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContact createContact)
        {
            await _commandHandler.HandleAsync(createContact);

            return Ok();
        }
    }
}

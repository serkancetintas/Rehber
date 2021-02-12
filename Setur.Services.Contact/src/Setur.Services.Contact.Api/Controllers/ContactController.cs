using Microsoft.AspNetCore.Mvc;
using Setur.Services.Contact.Api.Controllers.Base;
using Setur.Services.Contact.Application.Commands;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController: BaseApiController
    {
        private readonly ICommandHandler<CreateContact> _commandHandler;
        private readonly ICommandHandler<DeleteContact> _deleteCommandHandler;

        public ContactController(ICommandHandler<CreateContact> commandHandler,
                                 ICommandHandler<DeleteContact> deleteCommandHandler)
        {
            _commandHandler = commandHandler;
            _deleteCommandHandler = deleteCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] CreateContact createContact)
        {
            await _commandHandler.HandleAsync(createContact);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            await _deleteCommandHandler.HandleAsync(new DeleteContact(id));

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Setur.Services.Contact.Api.Controllers.Base;
using Setur.Services.Contact.Application.Commands;
using Setur.Services.Contact.Application.DTO;
using Setur.Services.Contact.Application.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController: BaseApiController
    {
        private readonly ICommandHandler<CreateContact> _commandHandler;
        private readonly ICommandHandler<DeleteContact> _deleteCommandHandler;
        private readonly ICommandHandler<AddContactInfo> _addContactInfoCommandHandler;
        private readonly ICommandHandler<DeleteContactInfo> _deleteContactInfoCommandHandler;
        private readonly IQueryHandler<GetContacts, IEnumerable<ContactDto>> _queryHandler;
        private readonly IQueryHandler<GetContactDetails, ContactDetailDto> _contactDetailQueryHandler;

        public ContactController(ICommandHandler<CreateContact> commandHandler,
                                 ICommandHandler<DeleteContact> deleteCommandHandler,
                                 ICommandHandler<AddContactInfo> addContactInfoCommandHandler,
                                 ICommandHandler<DeleteContactInfo> deleteContactInfoCommandHandler,
                                 IQueryHandler<GetContacts, IEnumerable<ContactDto>> queryHandler,
                                 IQueryHandler<GetContactDetails, ContactDetailDto> contactDetailQueryHandler
                                 )
        {
            _commandHandler = commandHandler;
            _deleteCommandHandler = deleteCommandHandler;
            _addContactInfoCommandHandler = addContactInfoCommandHandler;
            _deleteContactInfoCommandHandler = deleteContactInfoCommandHandler;
            _queryHandler = queryHandler;
            _contactDetailQueryHandler = contactDetailQueryHandler;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDto>> GetContacts()
        {
            return await _queryHandler.HandleAsync(new GetContacts());
        }

        [HttpGet("{id}")]
        public async Task<ContactDetailDto> GetContact([FromRoute] GetContactDetails getContactDetails)
        {
            return await _contactDetailQueryHandler.HandleAsync(getContactDetails);
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

        [HttpPut("AddContactInfo")]
        public async Task<IActionResult> AddContactInfo([FromBody] AddContactInfo addContactInfo)
        {
            await _addContactInfoCommandHandler.HandleAsync(addContactInfo);

            return Ok();
        }

        [HttpPut("DeleteContactInfo")]
        public async Task<IActionResult> DeleteContactInfo([FromBody] DeleteContactInfo deleteContactInfo)
        {
            await _deleteContactInfoCommandHandler.HandleAsync(deleteContactInfo);

            return Ok();
        }
    }
}

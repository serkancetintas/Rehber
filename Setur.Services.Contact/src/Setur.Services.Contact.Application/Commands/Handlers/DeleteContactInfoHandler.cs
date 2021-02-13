using Microsoft.Extensions.Logging;
using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using Setur.Services.Contact.Core.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Commands.Handlers
{
    public class DeleteContactInfoHandler:ICommandHandler<DeleteContactInfo>
    {
        private readonly IContactRepository _repository;
        private readonly ILogger<DeleteContactInfoHandler> _logger;

        public DeleteContactInfoHandler(IContactRepository repository,
                                     ILogger<DeleteContactInfoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteContactInfo command)
        {
            var contact = await _repository.GetAsync(command.ContactId);
            if (contact is null)
            {
                _logger.LogError($"Contact was not found with id: {command.ContactId}");
                throw new ContactNotFoundException(command.ContactId);
            }

            if (!Enum.TryParse<InfoType>(command.InfoType, true, out var infoType))
            {
                throw new InvalidInfoTypeException(command.InfoType);
            }

            contact.DeleteContactInfo(new ContactInfo(command.InfoContent, infoType, DateTime.Now));
            await _repository.UpdateAsync(contact);
        }
    }
}

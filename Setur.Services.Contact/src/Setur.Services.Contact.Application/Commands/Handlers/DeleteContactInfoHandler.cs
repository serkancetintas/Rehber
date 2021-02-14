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

        public DeleteContactInfoHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(DeleteContactInfo command)
        {
            var contact = await _repository.GetAsync(command.ContactId);
            if (contact is null)
            {
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

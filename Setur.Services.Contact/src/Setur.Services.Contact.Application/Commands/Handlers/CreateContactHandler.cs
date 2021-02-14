using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System;
using System.Threading.Tasks;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Application.Commands.Handlers
{
    public class CreateContactHandler : ICommandHandler<CreateContact>
    {
        private readonly IContactRepository _repository;
        public CreateContactHandler(IContactRepository repository)
                                   
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateContact command)
        {
            bool isExist = await _repository.IsExist(command.Name, command.Surname, command.CompanyName);
            if (isExist)
            {
                throw new ContactAlreadyExistException(command.Name, command.Surname, command.CompanyName);
            }

            var contact = new entity.Contact(Guid.NewGuid(), command.Name, command.Surname, command.CompanyName, DateTime.Now);

            await _repository.AddAsync(contact);
        }

    }
}

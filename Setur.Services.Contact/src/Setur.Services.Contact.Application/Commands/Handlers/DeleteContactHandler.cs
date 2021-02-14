using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Commands.Handlers
{
    public class DeleteContactHandler : ICommandHandler<DeleteContact>
    {
        private readonly IContactRepository _repository;
        public DeleteContactHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(DeleteContact command)
        {
            var contact = await _repository.GetAsync(command.Id);
            if (contact is null)
            {
                throw new ContactNotFoundException(command.Id);
            }

            await _repository.DeleteAsync(command.Id);
        }
    }
}

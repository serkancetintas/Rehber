using Microsoft.Extensions.Logging;
using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Commands.Handlers
{
    public class DeleteContactHandler : ICommandHandler<DeleteContact>
    {
        private readonly IContactRepository _repository;
        private readonly ILogger<DeleteContactHandler> _logger;
        public DeleteContactHandler(IContactRepository repository,
                                    ILogger<DeleteContactHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task HandleAsync(DeleteContact command)
        {
            var contact = await _repository.GetAsync(command.Id);
            if (contact is null)
            {
                _logger.LogError($"Contact was not found. Id: {command.Id}");
                throw new ContactNotFoundException(command.Id);
            }

            await _repository.DeleteAsync(command.Id);
        }
    }
}

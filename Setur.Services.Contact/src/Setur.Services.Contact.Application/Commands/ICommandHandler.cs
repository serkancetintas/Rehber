using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}

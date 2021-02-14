using Setur.Services.Contact.Application.Events;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
    }
}

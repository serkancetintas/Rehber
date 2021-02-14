using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Events
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}

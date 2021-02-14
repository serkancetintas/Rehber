using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    public interface IRabbitMqPlugin
    {
        Task HandleAsync(object message,  BasicDeliverEventArgs args);
    }
}

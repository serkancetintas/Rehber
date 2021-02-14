using Setur.Services.Report.Application.Events;
using System.Threading.Tasks;

namespace Setur.Services.Report.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] events);
    }
}

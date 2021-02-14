using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    internal interface IRabbitMqPluginAccessor
    {
        void SetSuccessor(Func<object, BasicDeliverEventArgs, Task> successor);
    }
}

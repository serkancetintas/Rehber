using System.Collections.Generic;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    public interface IRabbitMqClient
    {
        void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
            string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null);
    }
}
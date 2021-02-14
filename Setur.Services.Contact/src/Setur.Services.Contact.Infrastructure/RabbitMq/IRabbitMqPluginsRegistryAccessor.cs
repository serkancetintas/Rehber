using System.Collections.Generic;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    internal interface IRabbitMqPluginsRegistryAccessor
    {
        LinkedList<RabbitMqPluginChain> Get();
    }
}

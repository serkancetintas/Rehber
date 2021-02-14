using System.Collections.Generic;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    internal interface IRabbitMqPluginsRegistryAccessor
    {
        LinkedList<RabbitMqPluginChain> Get();
    }
}

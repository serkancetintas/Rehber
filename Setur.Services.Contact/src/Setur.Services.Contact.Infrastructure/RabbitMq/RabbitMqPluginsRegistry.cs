using System.Collections.Generic;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    internal sealed class RabbitMqPluginsRegistry : IRabbitMqPluginsRegistryAccessor
    {
        private readonly LinkedList<RabbitMqPluginChain> _plugins;

        public RabbitMqPluginsRegistry()
            => _plugins = new LinkedList<RabbitMqPluginChain>();

        LinkedList<RabbitMqPluginChain> IRabbitMqPluginsRegistryAccessor.Get()
            => _plugins;
    }
}

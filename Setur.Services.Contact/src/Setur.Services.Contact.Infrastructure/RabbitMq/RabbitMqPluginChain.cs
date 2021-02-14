using System;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    internal sealed class RabbitMqPluginChain
    {
        public Type PluginType { get; set; }
        public IRabbitMqPlugin Plugin { get; set; }
    }
}

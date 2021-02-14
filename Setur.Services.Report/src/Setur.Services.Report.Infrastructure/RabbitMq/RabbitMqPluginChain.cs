using System;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    internal sealed class RabbitMqPluginChain
    {
        public Type PluginType { get; set; }
        public IRabbitMqPlugin Plugin { get; set; }
    }
}

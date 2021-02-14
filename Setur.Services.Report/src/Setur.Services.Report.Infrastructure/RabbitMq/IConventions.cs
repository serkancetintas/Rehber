using System;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public interface IConventions
    {
        Type Type { get; }
        string RoutingKey { get; }
        string Exchange { get; }
        string Queue { get; }
    }
}
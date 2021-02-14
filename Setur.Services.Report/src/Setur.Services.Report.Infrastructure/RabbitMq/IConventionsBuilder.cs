using System;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public interface IConventionsBuilder
    {
        string GetRoutingKey(Type type);
        string GetExchange(Type type);
        string GetQueue(Type type);
    }
}
using System;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}
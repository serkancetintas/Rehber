using System;

namespace Setur.Services.Contact.Infrastructure.RabbitMq
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}
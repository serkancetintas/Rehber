using System;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Infrastructure.Services
{
    public interface IBusSubscriber : IDisposable
    {
        IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, Task> handle) where T : class;
    }
}

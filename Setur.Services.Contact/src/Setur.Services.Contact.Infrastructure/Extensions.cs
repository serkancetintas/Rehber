using Microsoft.Extensions.DependencyInjection;
using Setur.Services.Contact.Application.Queries;
using System;

namespace Setur.Services.Contact.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddQueryHandlers();

            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            services.Scan(s =>
               s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                   .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            return services;
        }
    }
}

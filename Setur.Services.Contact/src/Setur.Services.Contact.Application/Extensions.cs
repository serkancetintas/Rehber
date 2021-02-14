using Microsoft.Extensions.DependencyInjection;
using Setur.Services.Contact.Application.Commands;
using Setur.Services.Contact.Application.Events;
using Setur.Services.Contact.Application.Queries;
using System;

namespace Setur.Services.Contact.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommandHandlers();
            services.AddEventHandlers();
            services.AddQueryHandlers();

            return services;
        }

        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(s =>
               s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                   .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

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

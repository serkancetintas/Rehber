using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Setur.Services.Report.Application.Events;
using Setur.Services.Report.Application.Events.External;
using Setur.Services.Report.Application.Queries;
using Setur.Services.Report.Application.Services;
using Setur.Services.Report.Infrastructure.Exceptions;
using Setur.Services.Report.Infrastructure.RabbitMq;
using Setur.Services.Report.Infrastructure.Services;
using System;
using System.Linq;

namespace Setur.Services.Report.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessageBroker, MessageBroker>();

            services.AddQueryHandlers();
            services.AddRabbitMq(configuration);
            services.AddErrorHandler<ExceptionToResponseMapper>();

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

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseRabbitMq()
                .SubscribeEvent<ReportCompleted>();

            return app;
        }

        public static IServiceCollection AddErrorHandler<T>(this IServiceCollection services)
             where T : class, IExceptionToResponseMapper
        {
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionToResponseMapper, T>();

            return services;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
          => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static IBusSubscriber SubscribeEvent<T>(this IBusSubscriber busSubscriber) where T : class, IEvent
           => busSubscriber.Subscribe<T>(async (serviceProvider, @event) =>
           {
               using var scope = serviceProvider.CreateScope();
               await scope.ServiceProvider.GetRequiredService<IEventHandler<T>>().HandleAsync(@event);
           });

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
       where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }

        public static string Underscore(this string value)
      => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
          .ToLowerInvariant();
    }
}

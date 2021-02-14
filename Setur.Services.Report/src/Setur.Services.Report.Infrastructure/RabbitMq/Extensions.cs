using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using Setur.Services.Report.Infrastructure.RabbitMq.Clients;
using Setur.Services.Report.Infrastructure.RabbitMq.Conventions;
using Setur.Services.Report.Infrastructure.RabbitMq.Publishers;
using Setur.Services.Report.Infrastructure.RabbitMq.Subscribers;
using Setur.Services.Report.Infrastructure.Services;
using System;
using System.Linq;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public static class Extensions
    {
        private const string SectionName = "rabbitmq";

        public static IServiceCollection AddRabbitMq(this IServiceCollection services, 
                                                    IConfiguration configuration,
                                                    string sectionName = SectionName,
                                                    Action<ConnectionFactory> connectionFactoryConfigurator = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = configuration.GetOptions<RabbitMqOptions>(sectionName);
            services.AddSingleton(options);
           

            if (options.HostNames is null || !options.HostNames.Any())
            {
                throw new ArgumentException("RabbitMQ hostnames are not specified.", nameof(options.HostNames));
            }

            services.AddSingleton<IConventionsBuilder, ConventionsBuilder>();
            services.AddSingleton<IConventionsProvider, ConventionsProvider>();
            services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
            services.AddSingleton<IBusPublisher, RabbitMqPublisher>();
            services.AddSingleton<IBusSubscriber, RabbitMqSubscriber>();
            services.AddTransient<RabbitMqExchangeInitializer>();
           
            var pluginsRegistry = new RabbitMqPluginsRegistry();
            services.AddSingleton<IRabbitMqPluginsRegistryAccessor>(pluginsRegistry);
            var connectionFactory = new ConnectionFactory
            {
                Port = options.Port,
                VirtualHost = options.VirtualHost,
                UserName = options.Username,
                Password = options.Password
            };

            connectionFactoryConfigurator?.Invoke(connectionFactory);
            var connection = connectionFactory.CreateConnection(options.HostNames.ToList(), options.ConnectionName);
            services.AddSingleton(connection);
            var initializer = services.BuildServiceProvider().GetService<RabbitMqExchangeInitializer>();
            initializer.InitializeAsync();

            ((IRabbitMqPluginsRegistryAccessor)pluginsRegistry).Get().ToList().ForEach(p =>
              services.AddTransient(p.PluginType));

            return services;
        }

        


        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
           => new RabbitMqSubscriber(app.ApplicationServices);
    }
}

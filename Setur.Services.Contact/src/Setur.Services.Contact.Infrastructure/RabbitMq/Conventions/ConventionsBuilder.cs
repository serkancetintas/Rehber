using Setur.Services.Contact.Application.Services;
using System;
using System.Linq;
using System.Reflection;

namespace Setur.Services.Contact.Infrastructure.RabbitMq.Conventions
{
    public class ConventionsBuilder : IConventionsBuilder
    {
        private readonly RabbitMqOptions _options;
        private readonly bool _snakeCase;
        private readonly string _queueTemplate;

        public ConventionsBuilder(RabbitMqOptions options)
        {
            _options = options;
            _queueTemplate = string.IsNullOrWhiteSpace(_options.Queue?.Template)
                ? "{{assembly}}/{{exchange}}.{{message}}"
                : options.Queue.Template;
            _snakeCase = true;
        }

        public string GetRoutingKey(Type type)
        {
            var routingKey = type.Name;

            var attribute = GeAttribute(type);
            routingKey = string.IsNullOrWhiteSpace(attribute?.RoutingKey) ? routingKey : attribute.RoutingKey;

            return WithCasing(routingKey);
        }

        public string GetExchange(Type type)
        {
            var exchange = string.IsNullOrWhiteSpace(_options.Exchange?.Name)
                ? type.Assembly.GetName().Name
                : _options.Exchange.Name;


            var attribute = GeAttribute(type);
            exchange = string.IsNullOrWhiteSpace(attribute?.Exchange) ? exchange : attribute.Exchange;

            return WithCasing(exchange);
        }

        public string GetQueue(Type type)
        {
            var assembly = type.Assembly.GetName().Name;
            var message = type.Name;
            var exchange = _options.Exchange?.Name;

            var attribute = GeAttribute(type);
            exchange = string.IsNullOrWhiteSpace(attribute?.Exchange) ? exchange : attribute.Exchange;

            var queue = _queueTemplate.Replace("{{assembly}}", assembly)
                .Replace("{{exchange}}", exchange)
                .Replace("{{message}}", message);

            return WithCasing(queue);
        }

        private string WithCasing(string value) => _snakeCase ? SnakeCase(value) : value;

        private static string SnakeCase(string value)
            => string.Concat(value.Select((x, i) =>
                    i > 0 && value[i - 1] != '.' && value[i - 1] != '/' && char.IsUpper(x) ? "_" + x : x.ToString()))
                .ToLowerInvariant();

        private static MessageAttribute GeAttribute(MemberInfo type) => type.GetCustomAttribute<MessageAttribute>();
    }
}
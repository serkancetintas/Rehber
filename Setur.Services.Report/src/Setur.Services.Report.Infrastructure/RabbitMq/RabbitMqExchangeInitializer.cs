using RabbitMQ.Client;
using Setur.Services.Report.Application.Services;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public class RabbitMqExchangeInitializer
    {
        private const string DefaultType = "topic";
        private readonly IConnection _connection;
        private readonly RabbitMqOptions _options;

        public RabbitMqExchangeInitializer(IConnection connection, RabbitMqOptions options)
        {
            _connection = connection;
            _options = options;
        }

        public Task InitializeAsync()
        {
            var exchanges = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(typeof(MessageAttribute), false))
                .Select(t => t.GetCustomAttribute<MessageAttribute>().Exchange)
                .Distinct()
                .ToList();

            using var channel = _connection.CreateModel();
            if (_options.Exchange?.Declare == true)
            {
                channel.ExchangeDeclare(_options.Exchange.Name, _options.Exchange.Type, _options.Exchange.Durable,
                    _options.Exchange.AutoDelete);
            }

            foreach (var exchange in exchanges)
            {
                if (exchange.Equals(_options.Exchange?.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                channel.ExchangeDeclare(exchange, DefaultType, true);
            }

            channel.Close();

            return Task.CompletedTask;
        }


    }
}

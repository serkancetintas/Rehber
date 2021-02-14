using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Setur.Services.Contact.Infrastructure.Services;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Infrastructure.RabbitMq.Subscribers
{
    internal sealed class RabbitMqSubscriber : IBusSubscriber
    {
        private static readonly ConcurrentDictionary<string, ChannelInfo> Channels = new ConcurrentDictionary<string, ChannelInfo>();
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitMqPluginsRegistryAccessor _registry;
        private readonly IConnection _connection;
        private readonly IConventionsProvider _conventionsProvider;
        private readonly RabbitMqOptions _options;


        public RabbitMqSubscriber(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _connection = _serviceProvider.GetRequiredService<IConnection>();
            _conventionsProvider = _serviceProvider.GetRequiredService<IConventionsProvider>();
            _registry = _serviceProvider.GetRequiredService<IRabbitMqPluginsRegistryAccessor>();
            _options = _serviceProvider.GetRequiredService<RabbitMqOptions>();
        }

        public IBusSubscriber Subscribe<T>(Func<IServiceProvider, T, Task> handle)
           where T : class
        {
            var conventions = _conventionsProvider.Get<T>();
            var channelKey = $"{conventions.Exchange}:{conventions.Queue}:{conventions.RoutingKey}";
            if (Channels.ContainsKey(channelKey))
            {
                return this;
            }

            var channel = _connection.CreateModel();
            if (!Channels.TryAdd(channelKey, new ChannelInfo(channel, conventions)))
            {
                channel.Dispose();
                return this;
            }

            var declare = _options.Queue?.Declare ?? true;
            var durable = _options.Queue?.Durable ?? true;
            var exclusive = _options.Queue?.Exclusive ?? false;
            var autoDelete = _options.Queue?.AutoDelete ?? false;
            var info = string.Empty;

            if (declare)
            {
                channel.QueueDeclare(conventions.Queue, durable, exclusive, autoDelete);
            }

            channel.QueueBind(conventions.Queue, conventions.Exchange, conventions.RoutingKey);
            channel.BasicQos(0, 0, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, args) =>
            {
                try
                {
                    var messageId = args.BasicProperties.MessageId;
                    var payload = Encoding.UTF8.GetString(args.Body.Span);
                    var message = JsonConvert.DeserializeObject<T>(payload);

                    Task Next(object m, BasicDeliverEventArgs a)
                       => TryHandleAsync(channel, (T)m, messageId, a, handle);

                    await ExecuteAsync(Next, message, args);
                }
                catch (Exception ex)
                {
                    channel.BasicReject(args.DeliveryTag, false);
                    throw;
                }
            };

            channel.BasicConsume(conventions.Queue, false, consumer);

            return this;

        }

        private async Task TryHandleAsync<TMessage>(IModel channel, TMessage message, string messageId,
            BasicDeliverEventArgs args, Func<IServiceProvider, TMessage, Task> handle)
        {

            await handle(_serviceProvider, message);
            channel.BasicAck(args.DeliveryTag, false);
        }

        public async Task ExecuteAsync(Func<object, BasicDeliverEventArgs, Task> successor,
        object message, BasicDeliverEventArgs args)
        {
            var chains = _registry.Get();

            if (chains is null || !chains.Any())
            {
                await successor(message, args);
                return;
            }

            foreach (var chain in chains)
            {
                var plugin = _serviceProvider.GetService(chain.PluginType);

                if (plugin is null)
                {
                    throw new InvalidOperationException($"RabbitMq plugin of type {chain.PluginType.Name} was not registered");
                }

                chain.Plugin = plugin as IRabbitMqPlugin;
            }

            var current = chains.Last;

            while (current != null)
            {
                ((IRabbitMqPluginAccessor)current.Value.Plugin).SetSuccessor(current.Next is null
                    ? successor
                    : current.Next.Value.Plugin.HandleAsync);

                current = current.Previous;
            }

            await chains.First.Value.Plugin.HandleAsync(message, args);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        private class ChannelInfo : IDisposable
        {
            public IModel Channel { get; }
            public IConventions Conventions { get; }

            public ChannelInfo(IModel channel, IConventions conventions)
            {
                Channel = channel;
                Conventions = conventions;
            }

            public void Dispose()
            {
                Channel?.Dispose();
            }
        }
    }
}

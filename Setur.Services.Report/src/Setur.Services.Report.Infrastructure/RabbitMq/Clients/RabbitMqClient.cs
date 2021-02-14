using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Setur.Services.Report.Infrastructure.RabbitMq.Clients
{
    internal sealed class RabbitMqClient : IRabbitMqClient
    {
        private readonly object _lockObject = new object();
        private readonly IConnection _connection;
        private readonly ConcurrentDictionary<int, IModel> _channels = new ConcurrentDictionary<int, IModel>();
        private int _channelsCount;

        public RabbitMqClient(IConnection connection)
        {
            _connection = connection;
        }
        public void Send(object message, IConventions conventions, string messageId = null, string correlationId = null,
           string spanContext = null, object messageContext = null, IDictionary<string, object> headers = null)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            _channels.TryGetValue(threadId, out var channel);
            lock (_lockObject)
            {
                if (_channelsCount >= 1000)
                {
                    throw new InvalidOperationException($"Cannot create RabbitMQ producer channel for thread: {threadId} " +
                                                        $"(reached the limit of {1000} channels). ");
                }

                channel = _connection.CreateModel();
                _channels.TryAdd(threadId, channel);
                _channelsCount++;
            }

            var payload = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(payload);

            var properties = channel.CreateBasicProperties();
            properties.MessageId = string.IsNullOrWhiteSpace(messageId)
                ? Guid.NewGuid().ToString()
                : messageId;

            channel.BasicPublish(conventions.Exchange, conventions.RoutingKey, properties, body);
        }
    }
}

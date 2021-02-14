﻿using System.Collections.Generic;

namespace Setur.Services.Report.Infrastructure.RabbitMq
{
    public class RabbitMqOptions
    {
        public string ConnectionName { get; set; }
        public IEnumerable<string> HostNames { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ExchangeOptions Exchange { get; set; }
        public QueueOptions Queue { get; set; }
    }

    public class ExchangeOptions
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Declare { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
    }

    public class QueueOptions
    {
        public string Template { get; set; }
        public bool Declare { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
    }
}

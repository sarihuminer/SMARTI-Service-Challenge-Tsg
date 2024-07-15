using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Consumers
{
    public class RabbitMqConsumer : BackgroundService
    {
        private ILogger _logger { get; set; }

        //need ro implement
        //private readonly IEventBus _bus { get; set; }

        private string _demoCreateQueue;
        public RabbitMqConsumer(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _demoCreateQueue = configuration.GetSection("Queues").GetSection("DemoCreate").Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_bus.Subscribe
            _logger.LogInformation($"subscribed to DemoCreate queues");
            return Task.CompletedTask;
        }
    }
}

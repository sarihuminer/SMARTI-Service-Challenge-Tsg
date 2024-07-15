using Demo.Application.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class Binder
    {
        public static IServiceCollection AddApplicationFactory(this IServiceCollection services)
        {
            return services;
               // .AddHostedService<RabbitMqConsumer>();
        }
    }
}
using Demo.Application.Consumers;
using Demo.Application.Implementations;
using Demo.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class Binder
    {
        public static IServiceCollection AddApplicationFactory(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPrioritySettingsService, PrioritySettingsService>()
                .AddTransient<IMergeEntitiesService,MergeEntitiesService>();
            // .AddHostedService<RabbitMqConsumer>();
        }
    }
}
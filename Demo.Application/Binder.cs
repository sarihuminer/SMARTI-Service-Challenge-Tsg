using Demo.Application.Consumers;
using Demo.Application.Implementations;
using Demo.Application.Implementations.Proxies;
using Demo.Application.Interfaces;
using Demo.Application.Interfaces.Proxies;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Application
{
    public static class Binder
    {
        public static IServiceCollection AddApplicationFactory(this IServiceCollection services)
        {
            return services
                .AddScoped<IGitHubSearchService,GitHubSearchService>()
                .AddSingleton<IPrioritySettingsService, PrioritySettingsService>()
                .AddTransient<IMergeEntitiesService,MergeEntitiesService>();
            // .AddHostedService<RabbitMqConsumer>();
        }
    }
}
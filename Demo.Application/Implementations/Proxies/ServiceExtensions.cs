using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Implementations.Proxies
{
    // ServiceExtensions.cs
    public static class ServiceExtensions
    {
        public static IServiceCollection Configure<TInterface, TImplementation>(
            this IServiceCollection services,
            string baseUrl,
            bool isDefaultCredentials
        ) where TInterface : class where TImplementation : class, TInterface
        {
            IHttpClientBuilder httpClientBuilder = services.AddHttpClient<TInterface, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            if (isDefaultCredentials)
            {
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    UseDefaultCredentials = true
                });
            }

            return services;
        }
    }

}

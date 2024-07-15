using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public static class Binder
    {
        public static IServiceCollection AddRepositoriesFactory(this IServiceCollection services)
        {
            // services.AddDbContext<DemoContex>();

            //add repositories and interfaces
            // services.AddScoped<,>

            //services.AddTransient<IUnitOfWork>(provider=>
            //new UnitOfWork)
            return services;
        }
    }
}

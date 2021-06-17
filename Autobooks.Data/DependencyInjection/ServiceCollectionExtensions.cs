using Autobooks.Common.Configuration;
using Autobooks.Data.DbContext;
using Autobooks.Data.DbContext.Interfaces;
using Autobooks.Data.Models;
using Autobooks.Data.Repositories;
using Autobooks.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Autobooks.Data.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAutobooksDataServices(this IServiceCollection services)
        {
            services.AddScoped<IGroceryDbContext>((provider) => new GroceryDbContext(provider.GetRequiredService<GroceryConfig>().DatabasePath));
            services.AddScoped<IGroceryRepository<Customer>, GroceryRepository<Customer>>();

            return services;
        }
    }
}

using Autobooks.Business.Services;
using Autobooks.Business.Services.Interfaces;
using Autobooks.Data.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Autobooks.Business.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAutobooksBusinessServices(this IServiceCollection services)
        {
            services.ConfigureAutobooksDataServices();
            services.AddScoped<ICustomersService, CustomersService>();

            return services;
        }
    }
}

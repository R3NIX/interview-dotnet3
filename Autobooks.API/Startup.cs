using Autobooks.API.Automapper;
using Autobooks.API.Middleware;
using Autobooks.Business.DependencyInjection;
using Autobooks.Common.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Autobooks.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public GroceryConfig GroceryConfig { get; set; }

        /* Used by the runtime to add new services to the container.*/
        public void ConfigureServices(IServiceCollection services)
        {
            var groceryConfig = new GroceryConfig();
            Configuration.GetSection("Grocery").Bind(groceryConfig);
            GroceryConfig = groceryConfig;

            services.AddSingleton<GroceryConfig>(x => groceryConfig);

            /*Automapper Setup*/
            var automapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddAutobooksMappingProfiles();
            });

            services.ConfigureAutobooksBusinessServices();
            services.AddScoped<IMapper>(x => automapperConfig.CreateMapper());

            services.AddControllers(configure => configure.Filters.Add(typeof(ExceptionLogger)));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Autobooks.API", Version = "v1" });
            });
        }

        /* Used by the runtime to configure the HTTP request pipeline. */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autobooks.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

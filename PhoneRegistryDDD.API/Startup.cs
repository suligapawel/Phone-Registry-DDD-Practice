using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneRegistryDDD.Availability.Api;
using PhoneRegistryDDD.Helpdesk.Api;
using PhoneRegistryDDD.Orchestrating;
using PhoneRegistryDDD.Warehouse.Api;

namespace PhoneRegistryDDD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddAvailability(Configuration)
                .AddHelpdesk(Configuration)
                .AddWarehouse()
                .AddOrchestrating();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
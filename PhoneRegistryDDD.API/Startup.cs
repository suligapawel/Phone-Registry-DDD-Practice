using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneRegistryDDD.Availability.Core.Repositories;
using PhoneRegistryDDD.Availability.Infrastructure.Handlers;
using PhoneRegistryDDD.Availability.Infrastructure.Repositories;
using PhoneRegistryDDD.Helpdesk.Core.Repositories;
using PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Handlers;
using PhoneRegistryDDD.Helpdesk.Infrastructure.Repositories;

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
            services.AddMediatR(typeof(UnblockAssortmentHandler).Assembly,
                typeof(TakeBackKitHandler).Assembly);

            AddRepositories(services);
            AddDbContext(services);
        }

        private void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<HelpdeskContext>(options => options.UseSqlServer(Configuration.GetConnectionString("helpdesk")));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IAssortmentRepository, AssortmentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using PhoneRegistryDDD.API.Extensions;
using PhoneRegistryDDD.Shared.Infrastructure;
using SuligaPawel.Common.Exceptions.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddModuleSettings();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseExceptionMiddleware();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();

// It's workaround for integration tests
#pragma warning disable CA1050
public partial class Program
#pragma warning restore CA1050
{
}
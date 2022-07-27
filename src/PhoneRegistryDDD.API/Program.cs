using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PhoneRegistryDDD.API;
using PhoneRegistryDDD.Shared.Infrastructure;

CreateHostBuilder(args)
    .Build()
    .RunAsync();

static IHostBuilder CreateHostBuilder(string[] args)
    => Host
        .CreateDefaultBuilder(args)
        .AddModuleSettings()
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
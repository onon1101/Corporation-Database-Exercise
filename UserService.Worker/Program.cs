using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Worker.Repositories;
using UserService.Worker.Services;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using UserService;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true);
    })
    .ConfigureServices((ctx, services) =>
    {
        services = UserService.ServiceCollectionExtension.AddAppService(services);
    })
    .Build()
    .Run();
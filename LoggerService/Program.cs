using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.Extensions.Hosting;
using LoggerService;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfigurationFromJson()
    .ConfigureServicesFromContext()
    .Build()
    .Run();


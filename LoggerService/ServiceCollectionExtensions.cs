using Serilog;
using Serilog.AspNetCore;
using MongoDB.Driver;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        services.AddLogging(static loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog();
        });

        return services;
    }

    public static IHostBuilder ConfigureServicesFromContext(this IHostBuilder builder)
    {
        return builder.ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;

            // 加入 Logger
            services = AddAppService(services);

            // 加入 MongoDB 服務
            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration["MongoDb:ConnectionString"];
                return new MongoClient(connectionString);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var dbName = configuration["MongoDb:DatabaseName"];
                return client.GetDatabase(dbName);
            });

            // 加入背景服務
            services.AddHostedService<MongoSampleService>();
        });
    }
}

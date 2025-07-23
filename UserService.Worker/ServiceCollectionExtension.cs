using System.Data;
using System.Text;
using Npgsql;
using UserService.Worker.Repositories;
using UserService.Worker.Services;

namespace UserService;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        services.AddScoped<IDbConnection>(provider => new NpgsqlConnection("Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=postgres"));
        services.AddScoped<UserRepository>();
        services.AddHostedService<KafkaConsumerService>();
        
        return services;
    }
}

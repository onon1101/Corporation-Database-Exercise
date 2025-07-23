namespace LoggerService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppService(this IServiceCollection services)
    {
        // services.AddScoped<IDbConnection>(provider => new NpgsqlConnection("Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=postgres"));
        // services.AddScoped<UserRepository>();
        // services.AddHostedService<KafkaConsumerService>();
        
        return services;
    }
}
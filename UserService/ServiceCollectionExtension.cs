using System.Data;
using System.Text;
using Api.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using StackExchange.Redis;
using UserService.Eventing;
using UserService.Repositories;
using UserService.Repositories.Interface;
using UserService.Services;

namespace UserService;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAppService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect("localhost:6379"));
        services.AddScoped<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        
        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<UserRepository>();
        services.AddScoped<IKafkaTopicResolver, KafkaTopicResolver>();
        services.AddScoped<KafkaProducer>();
        return services;
    }
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });
        return services;
    }
}

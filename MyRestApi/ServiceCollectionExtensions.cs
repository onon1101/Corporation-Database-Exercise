using Microsoft.Extensions.DependencyInjection;
using MyRestApi.Repositories;
using MyRestApi.Services;
using System.Data;
using Npgsql;

namespace MyRestApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

            // User
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Theater
            services.AddScoped<ITheaterService, TheaterService>();
            services.AddScoped<ITheaterRepository, TheaterRepository>();

            // Movie
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            // Schedule
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IScheduleService, ScheduleService>();

            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<ISeatService, SeatService>();

            // Reservation and Reservation_seat
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }
    }
}
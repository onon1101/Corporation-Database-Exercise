using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyRestApi.Utils;

namespace Utils
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureEnvironmentAndDatabase(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsEnvironment("Test"))
            {
                builder.Configuration.AddJsonFile("appsettings.Test.json");
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                DbMigration.EnsureDatabase(connectionString);
            }
        }
    }
}
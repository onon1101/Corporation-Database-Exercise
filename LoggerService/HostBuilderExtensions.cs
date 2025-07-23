namespace LoggerService;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAppConfigurationFromJson(this IHostBuilder builder)
    {
        return builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        });
    }
}

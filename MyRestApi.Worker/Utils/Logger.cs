
using Serilog;

namespace Utils;

public class Logger
{
    public static void Init()
    {
        Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Warning() // 設定最低等級
            .MinimumLevel.Information() // 設定最低等級
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
    }
}
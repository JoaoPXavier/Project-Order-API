using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace OrdersApi.Shared.Logging
{
    public static class SerilogConfig
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
            });
        }
    }
}

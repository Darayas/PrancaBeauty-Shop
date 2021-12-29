using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace PrancaBeauty.Infrastructure.Logger.Serilog
{
    public static class SeriLogEx
    {
        public static void UseSeriLog_SqlServer(this ConfigureHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, logger) =>
            {
                logger = new SerilogConfig().ConfigSqlServer(LogEventLevel.Warning);
                logger.CreateLogger();
            });
        }

        public static void UseSeriLog_Console(this ConfigureHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, logger) =>
            {
                logger.WriteTo.Console().MinimumLevel.Is(LogEventLevel.Verbose);
            });
        }
    }
}


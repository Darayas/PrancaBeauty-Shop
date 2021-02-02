using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.Logger.Serilog
{
    public static class SeriLogEx
    {
        public static void UseSeriLog_SqlServer(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, logger) =>
            {
                logger = new SerilogConfig().ConfigSqlServer(LogEventLevel.Warning);
            });
        }

        public static void UseSeriLog_Console(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseSerilog((builder, logger) =>
            {
                logger.WriteTo.Console().MinimumLevel.Is(LogEventLevel.Verbose);
            });
        }
    }
}


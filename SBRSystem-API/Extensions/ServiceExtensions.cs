using Serilog;

namespace SBRSystem_API.Extensions;

public static class ServiceExtensions
{
    
        public static void ConfigurarLogger(this IServiceCollection services)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logfile.log", rollingInterval: RollingInterval.Day)
                
                .CreateLogger();

        }
}
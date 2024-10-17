using Serilog;

namespace SBRSystem_API.Extensions;

public static class ServiceExtensions
{
    
        public static void ConfigurarLogger(this IServiceCollection services)
        {

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                
                .CreateLogger();

        }
        
        public static void ConfigurarCORS(this IServiceCollection services, string MyAllowSpecifiOrigins)
        {
            string[] domains = {  "http://localhost:4000",  };


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecifiOrigins,
                    policy =>
                    {
                        policy.WithOrigins(domains)
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }
        
        
}
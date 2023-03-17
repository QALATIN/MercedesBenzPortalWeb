using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MercedesBenzLogger;
using Serilog;

namespace MercedesBenzApiIneValidacion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext();
                    configuration.ReadFrom.Configuration(context.Configuration);
                    configuration.Enrich.With(CustomLogger.CustomLoggerSerilog());
                })
            ;
    }
}

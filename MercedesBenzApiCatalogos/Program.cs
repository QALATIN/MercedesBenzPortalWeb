using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MercedesBenzLogger;
using Serilog;

namespace MercedesBenzApiCatalogos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = CustomLogger.CustomLoggerConfiguration();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
            ;
    }
}

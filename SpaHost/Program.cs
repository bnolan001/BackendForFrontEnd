using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace SpaHost
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                        .ConfigureAppConfiguration(config =>
                        {
                            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                            config
                                // Used for local settings like connection strings.
                                .AddJsonFile("appsettings.json", false, true)
                                .AddJsonFile($"appsettings.{environment}.json", false, true);
                        })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
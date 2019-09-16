using DotNetCore_WebJob_SdkV3.Services;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetCore_WebJob_SdkV3
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
               .ConfigureWebJobs(configure =>
               {
                   configure
                   .AddAzureStorageCoreServices()
                   .AddAzureStorage()
                   .AddTimers();
               })
               .ConfigureAppConfiguration(b =>
               {
                   // Adding command line as a configuration source
                   b.AddCommandLine(args);
               })
               .ConfigureLogging((context, b) =>
               {
                   b.SetMinimumLevel(LogLevel.Debug);
                   b.AddConsole();

                   // If this key exists in any config, use it to enable App Insights
                   string appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                   if (!string.IsNullOrEmpty(appInsightsKey))
                   {
                       b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = appInsightsKey);
                   }
               })
               .ConfigureServices(services =>
               {
                   services.AddScoped<ICleanupService, CleanupService>();
                   services.AddSingleton<TelemetryClient>();
               })
               .UseConsoleLifetime();

            using (var host = builder.Build())
            {
                await host.RunAsync();
            }
        }
    }
}

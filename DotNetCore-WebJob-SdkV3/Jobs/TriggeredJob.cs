using DotNetCore_WebJob_SdkV3.Services;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore_WebJob_SdkV3.Jobs
{
    public class TriggeredJob
    {
        private const string Daily = "0 0 0 * * *";
        private readonly ICleanupService _cleanupService;

        public TriggeredJob(
            ICleanupService cleanupService)
        {
            _cleanupService = cleanupService;
        }

        [FunctionName("webjob-triggered-cleanup")]
        public async Task RunAsync(
            [TimerTrigger(Daily, RunOnStartup = true)] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            // DI on function level IS possible but requires effort: https://blog.wille-zone.de/post/azure-functions-proper-dependency-injection/
            // easier options:
            // 1. Use ConfigureServices of HostBuilder and inject via ctor
            await _cleanupService.CleanupAsync(cancellationToken);
            // 2. roll your own in the function
            var sp = GetServiceProvider();
            await sp.GetRequiredService<ICleanupService>().CleanupAsync(cancellationToken);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddTransient<ICleanupService, CleanupService>();
            services.AddSingleton(new TelemetryClient());

            return services.BuildServiceProvider();
        }
    }
}

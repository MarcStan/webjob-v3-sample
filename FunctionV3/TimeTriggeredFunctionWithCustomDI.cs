using Microsoft.ApplicationInsights;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using SharedLogic.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebJob_SdkV3.Jobs
{
    /// <summary>
    /// Function with custom DI inside the function.
    /// Services will always be resolved per function call (singleton and scoped services resolve the same)
    /// </summary>
    public class TimeTriggeredFunctionWithCustomDI
    {
        private const string Daily = "0 0 0 * * *";

        [FunctionName("triggered-function-with-custom-di-in-function")]
        public async Task CleanAsync(
            [TimerTrigger(Daily, RunOnStartup = true)] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            // DI on function level IS possible but requires larger effort: https://blog.wille-zone.de/post/azure-functions-proper-dependency-injection/
            var sp = GetServiceProvider();
            await sp.GetRequiredService<ICleanupService>().CleanupAsync(cancellationToken);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddTransient<ICleanupService, CleanupService>();
            // singleton and scoped will be identical because serivce provider is always recreated per function call)
            services.AddSingleton(new TelemetryClient());

            return services.BuildServiceProvider();
        }
    }
}

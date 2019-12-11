using Microsoft.Azure.WebJobs;
using SharedLogic.Services;
using System.Threading;
using System.Threading.Tasks;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace WebJob_SdkV3.Jobs
{
    /// <summary>
    /// Function with DI via function parameter (requires external nuget package and service setup via Startup).
    /// Quite the setup for very limited DI: https://blog.wille-zone.de/post/dependency-injection-for-azure-functions/
    /// Services will always be resolved per function call.
    /// </summary>
    public class TimeTriggeredFunctionWithBindingDI
    {
        private const string Daily = "0 0 0 * * *";

        [FunctionName("triggered-function-with-di-via-function-arguments")]
        public async Task CleanAsync(
            [TimerTrigger(Daily, RunOnStartup = true)] TimerInfo myTimer,
            CancellationToken cancellationToken,
            [Inject] ICleanupService cleanupService)
        {
            await cleanupService.CleanupAsync(cancellationToken);
        }
    }
}

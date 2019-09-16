using Microsoft.Azure.WebJobs;
using SharedLogic.Services;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore_WebJob_SdkV3.Jobs
{
    /// <summary>
    /// Using ConfigureServices of HostBuilder in Program.cs allows injecting services via ctor of function classes.
    /// </summary>
    public class TimeTriggeredJobWithHostDI
    {
        private const string Daily = "0 0 0 * * *";
        private readonly ICleanupService _cleanupService;

        public TimeTriggeredJobWithHostDI(
            ICleanupService cleanupService)
        {
            _cleanupService = cleanupService;
        }

        [FunctionName("triggered-webjob-with-host-di")]
        public async Task CleanAsync(
            [TimerTrigger(Daily, RunOnStartup = true)] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            await _cleanupService.CleanupAsync(cancellationToken);
        }
    }
}

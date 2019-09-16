using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetCore_FunctionV2
{
    public static class Functions
    {
        private const string Daily = "0 0 0 * * *";

        [FunctionName("function-triggered-cleanup")]
        public static async Task TimeTriggeredAsync(
            [TimerTrigger(Daily, RunOnStartup = true)]TimerInfo myTimer,
            ILogger log)
        {
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore_WebJob_SdkV3.Services
{
    public interface ICleanupService
    {
        Task CleanupAsync(CancellationToken cancellationToken);
    }
}

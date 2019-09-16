using System.Threading;
using System.Threading.Tasks;

namespace SharedLogic.Services
{
    public interface ICleanupService
    {
        Task CleanupAsync(CancellationToken cancellationToken);
    }
}

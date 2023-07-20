using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;

namespace Depra.Assets.IO.Rules
{
    public interface IReadRule<TData>
    {
        bool CanRead();
        
        TData Read();

        Task<TData> ReadAsync(DownloadProgressDelegate onProgress = null,
            CancellationToken cancellationToken = default);
    }
}
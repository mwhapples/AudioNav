using System.Threading;
using System.Threading.Tasks;

namespace AudioNav.Models;

public interface IAudioCompassOutput
{
    public string Name { get; }
    public Task RunAsync(AudioCompassOutputData outputData, CancellationToken cancellationToken);
}

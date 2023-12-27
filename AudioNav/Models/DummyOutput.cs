using System;
using System.Threading;
using System.Threading.Tasks;

namespace AudioNav.Models;

public class DummyOutput : IAudioCompassOutput
{
    public string Name { get; } = "No output";
    public async Task RunAsync(AudioCompassOutputData outputData, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromDays(1));
            }
            catch (OperationCanceledException) { }
        }
    }
}

using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace AudioNav.Models;

public interface IAudioCompassOutput
{
    public string Name { get; }
    public IObservable<Unit> CreateOutputObservable(AudioCompassOutputData outputData);
}

using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioNav.Models;

public class DummyOutput : IAudioCompassOutput
{
    public string Name { get; } = "No output";
    public IObservable<Unit> CreateOutputObservable(AudioCompassOutputData outputData) => Observable.Never<Unit>();
}

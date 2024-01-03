using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.Models;

internal class DummyCompassProvider : ICompassProvider
{
    public string Name { get; } = "Dummy compass";
    public bool IsSupported { get; } = true;
    public IObservable<CompassData> CompassObservable { get; } = new BehaviorSubject<CompassData>(new CompassData.NoHeading()).AsObservable();
}

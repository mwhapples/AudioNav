using System;

namespace AudioNav.Models;

public interface ICompassProvider
{
    public string Name { get; }
    public bool IsSupported { get; }
    IObservable<CompassData> CompassObservable { get; }
}

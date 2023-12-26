using System;

namespace AudioNav.Models;

public interface ICompassProvider
{
    public string Name { get; }
    IObservable<CompassData> CompassObservable { get; }
}

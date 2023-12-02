using System;

namespace AudioNav.Models;

public interface ICompassProvider
{
    IObservable<CompassData> CompassObservable { get; }
}

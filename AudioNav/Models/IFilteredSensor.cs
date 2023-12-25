using System;

namespace AudioNav.Models;

public interface IFilteredSensor
{
    public IObservable<double> FilterRate { get; }
    public void ChangeFilterRate(double filterRate);
}

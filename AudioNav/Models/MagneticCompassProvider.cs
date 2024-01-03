using AudioNav.Utils;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.Models;

internal class MagneticCompassProvider : ICompassProvider, IFilteredSensor
{
    private ISubject<double> filterRate = new BehaviorSubject<double>(0.1);
    public MagneticCompassProvider()
    {
        CompassObservable = Observable.FromEventPattern<CompassChangedEventArgs>(
        h =>
        {
            Compass.Default.ReadingChanged += h;
            Compass.Default.Start(SensorSpeed.Default, applyLowPassFilter: false);
        },
        h =>
        {
            Compass.Default.Stop();
            Compass.Default.ReadingChanged -= h;
        }).Select(x => new CompassData.HeadingReading(Heading.FromDegrees(x.EventArgs.Reading.HeadingMagneticNorth))).LowPassFilter(FilterRate);
    }
    public IObservable<double> FilterRate => filterRate.AsObservable();
    public void ChangeFilterRate(double filterRate) => this.filterRate.OnNext(filterRate);
    public IObservable<CompassData> CompassObservable { get; }
    public string Name { get; } = "Magnetic compass";
    public bool IsSupported => Compass.IsSupported;
}

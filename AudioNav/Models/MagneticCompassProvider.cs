using Microsoft.Maui.Devices.Sensors;
using System;
using System.Reactive.Linq;

namespace AudioNav.Models;

internal class MagneticCompassProvider : ICompassProvider
{
    public IObservable<CompassData> CompassObservable { get; } = Observable.FromEventPattern<CompassChangedEventArgs>(
        h =>
        {
            Compass.Default.ReadingChanged += h;
            Compass.Default.Start(SensorSpeed.Default, applyLowPassFilter: false);
        },
        h =>
        {
            Compass.Default.Stop();
            Compass.Default.ReadingChanged -= h;
        }).Select(x => new CompassData.HeadingReading(Heading.FromDegrees(x.EventArgs.Reading.HeadingMagneticNorth)));
}

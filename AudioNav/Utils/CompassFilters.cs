using AudioNav.Models;
using System;
using System.Reactive.Linq;

namespace AudioNav.Utils;

internal static class CompassFilters
{
    public static IObservable<CompassData> LowPassFilter(this IObservable<CompassData> compassObservable, IObservable<double> compassFilterRateObservable) => compassObservable.WithLatestFrom(compassFilterRateObservable).Scan((CompassData)new CompassData.NoHeading(), (acc, x) => x.First switch
        {
            CompassData.HeadingReading h1 => acc is CompassData.HeadingReading h2 ? new CompassData.HeadingReading(Heading.FromComplex((h1.Heading.ToComplex() * (1 - x.Second)) + (h2.Heading.ToComplex() * x.Second))) : h1,
            _ => x.First
        });
}

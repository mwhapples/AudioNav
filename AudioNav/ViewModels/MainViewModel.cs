using AudioNav.Models;
using AudioNav.Utils;
using ReactiveUI;
using System;
using System.Numerics;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public MainViewModel()
    {
        compass = new MagneticCompassProvider().CompassObservable.ToProperty(this, x => x.Compass);
        var compassFilterRateObservable = this.WhenAnyValue(x => x.CompassFilterRate);
        filteredCompass = this.WhenAnyValue(x => x.Compass).WithLatestFrom(compassFilterRateObservable).Scan((CompassData)new CompassData.NoHeading(), (acc, x) => x.First switch
        {
            CompassData.HeadingReading h1 => acc is CompassData.HeadingReading h2 ? new CompassData.HeadingReading(Heading.FromComplex(h1.Heading.ToComplex() + (h2.Heading.ToComplex() * x.Second))) : h1,
            _ => x.First
        }).ToProperty(this, x => x.FilteredCompass);
        headingText = this.WhenAnyValue(x => x.FilteredCompass).Select(x => x switch
        {
            CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
            _ => "---"
        }).ToProperty(this, x => x.HeadingText);
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<CompassData> compass;
    public CompassData Compass => compass.Value;
    private int compassFilterRate = 10;
    public int CompassFilterRate
    {
        get => compassFilterRate;
        set => this.RaiseAndSetIfChanged(ref compassFilterRate, value);
    }
    private readonly ObservableAsPropertyHelper<CompassData> filteredCompass;
    public CompassData FilteredCompass => filteredCompass.Value;
    private readonly ObservableAsPropertyHelper<string> headingText;
    public string HeadingText => headingText.Value;
}

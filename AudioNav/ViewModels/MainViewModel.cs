using AudioNav.Models;
using AudioNav.Utils;
using AudioNav.Views;
using ReactiveUI;
using Splat;
using System;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public AudioCompass AudioCompass { get; }
    public MainViewModel()
    {
        AudioCompass = new();
        Locator.CurrentMutable.Register(() => new SimpleHeadingView(), typeof(IViewFor<SimpleHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleCourseView(), typeof(IViewFor<SimpleCourseViewModel>));
        compassFilterRate = AudioCompass.FilterRate.ToProperty(this, x => x.CompassFilterRate);
        filteredCompass = AudioCompass.CompassHeading.ToProperty(this, x => x.FilteredCompass);
    }

    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<int> compassFilterRate;
    public int CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => AudioCompass.ChangeFilterRate(value);
    }
    private readonly ObservableAsPropertyHelper<CompassData> filteredCompass;
    public CompassData FilteredCompass => filteredCompass.Value;
}

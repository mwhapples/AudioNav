using AudioNav.Models;
using AudioNav.Views;
using ReactiveUI;
using Splat;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private ObservableAsPropertyHelper<object?> filterRate;
    private object audioOutput;
    public MainViewModel()
    {
        AudioCompass = new();
        audioOutput = new SpeakAbsoluteHeadingViewModel(AudioCompass);
        filterRate = AudioCompass.CompassProvider.Select(x => x is IFilteredSensor ? new FilterRateViewModel((IFilteredSensor)x) : null).ToProperty(this, x => x.FilterRate);
        this.WhenActivated(disposables =>
        {
            filterRate.DisposeWith(disposables);
        });
        Locator.CurrentMutable.Register(() => new SpeakAbsoluteHeadingView(), typeof(IViewFor<SpeakAbsoluteHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleHeadingView(), typeof(IViewFor<SimpleHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleCourseView(), typeof(IViewFor<SimpleCourseViewModel>));
        Locator.CurrentMutable.Register(() => new CourseToHeadingView(), typeof(IViewFor<CourseToHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new FilterRateView(), typeof(IViewFor<FilterRateViewModel>));
        Locator.CurrentMutable.Register(() => new SensorSelectorView(), typeof(IViewFor<SensorSelectorViewModel>));
    }

    public ViewModelActivator Activator { get; } = new();
    public AudioCompass AudioCompass { get; }
   
    public object AudioOutput {
        get => audioOutput;
        set => this.RaiseAndSetIfChanged(ref audioOutput, value);
    }
    public object? FilterRate => filterRate.Value;
}

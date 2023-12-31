using AudioNav.Models;
using AudioNav.Views;
using ReactiveUI;
using Splat;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ObservableAsPropertyHelper<object?> filterRate;
    public MainViewModel()
    {
        AudioCompass = new();
        filterRate = AudioCompass.CompassProvider.Select(x => x is IFilteredSensor ? new FilterRateViewModel((IFilteredSensor)x) : null).ToProperty(this, x => x.FilterRate);
        this.WhenActivated(disposables =>
        {
            AudioCompass.DisposeWith(disposables);
        });
        Locator.CurrentMutable.Register(() => new OutputSelectorView(), typeof(IViewFor<OutputSelectorViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleHeadingView(), typeof(IViewFor<SimpleHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleCourseView(), typeof(IViewFor<SimpleCourseViewModel>));
        Locator.CurrentMutable.Register(() => new CourseToHeadingView(), typeof(IViewFor<CourseToHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new FilterRateView(), typeof(IViewFor<FilterRateViewModel>));
        Locator.CurrentMutable.Register(() => new SensorSelectorView(), typeof(IViewFor<SensorSelectorViewModel>));
    }

    public ViewModelActivator Activator { get; } = new();
    public AudioCompass AudioCompass { get; }
    public object? FilterRate => filterRate.Value;
}

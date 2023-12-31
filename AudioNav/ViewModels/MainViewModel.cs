using AudioNav.Models;
using AudioNav.Views;
using ReactiveUI;
using Splat;
using System.Collections.Immutable;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public MainViewModel()
    {
        AudioCompass = new();
        
        ViewModels = [new SensorSelectorViewModel(AudioCompass), new OutputSelectorViewModel(AudioCompass), new SimpleHeadingViewModel(AudioCompass), new SimpleCourseViewModel(AudioCompass), new CourseToHeadingViewModel(AudioCompass), new FilterRateViewModel(AudioCompass)];
        Locator.CurrentMutable.Register(() => new OutputSelectorView(), typeof(IViewFor<OutputSelectorViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleHeadingView(), typeof(IViewFor<SimpleHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleCourseView(), typeof(IViewFor<SimpleCourseViewModel>));
        Locator.CurrentMutable.Register(() => new CourseToHeadingView(), typeof(IViewFor<CourseToHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new FilterRateView(), typeof(IViewFor<FilterRateViewModel>));
        Locator.CurrentMutable.Register(() => new SensorSelectorView(), typeof(IViewFor<SensorSelectorViewModel>));
    }

    public ViewModelActivator Activator { get; } = new();
    public AudioCompass AudioCompass { get; }
    public ImmutableList<object> ViewModels {  get; }
}

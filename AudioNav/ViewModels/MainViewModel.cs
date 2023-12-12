using AudioNav.Models;
using AudioNav.Views;
using ReactiveUI;
using Splat;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    public MainViewModel()
    {
        AudioCompass = new();
        audioOutput = new SpeakAbsoluteHeadingViewModel(AudioCompass);
        Locator.CurrentMutable.Register(() => new SpeakAbsoluteHeadingView(), typeof(IViewFor<SpeakAbsoluteHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleHeadingView(), typeof(IViewFor<SimpleHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new SimpleCourseView(), typeof(IViewFor<SimpleCourseViewModel>));
        Locator.CurrentMutable.Register(() => new CourseToHeadingView(), typeof(IViewFor<CourseToHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new FilterRateView(), typeof(IViewFor<FilterRateViewModel>));
    }

    public ViewModelActivator Activator { get; } = new();
    public AudioCompass AudioCompass { get; }
    private object audioOutput;
    public object AudioOutput {
        get => audioOutput;
        set => this.RaiseAndSetIfChanged(ref audioOutput, value);
    }
}

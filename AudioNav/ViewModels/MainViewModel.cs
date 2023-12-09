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
        Locator.CurrentMutable.Register(() => new CourseToHeadingView(), typeof(IViewFor<CourseToHeadingViewModel>));
        Locator.CurrentMutable.Register(() => new FilterRateView(), typeof(IViewFor<FilterRateViewModel>));
    }

    public ViewModelActivator Activator { get; } = new();
}

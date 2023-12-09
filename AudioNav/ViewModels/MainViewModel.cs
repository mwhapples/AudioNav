using AudioNav.Models;
using AudioNav.Utils;
using ReactiveUI;
using System;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly Audio_compass audio_Compass;
    public MainViewModel()
    {
        audio_Compass = new();
        course = audio_Compass.Course.Select(x => (int)x.Degrees).ToProperty(this, x => x.Course);
        compassFilterRate = audio_Compass.FilterRate.ToProperty(this, x => x.CompassFilterRate);
        filteredCompass = audio_Compass.CompassHeading.ToProperty(this, x => x.FilteredCompass);
        headingText = this.WhenAnyValue(x => x.FilteredCompass).Select(x => x switch
        {
            CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
            _ => "---"
        }).ToProperty(this, x => x.HeadingText);
        IncrementCourseCommand = ReactiveCommand.Create<int>(x => Course += x);
    }

    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<int> course;
    public int Course
    {
        get => course.Value;
        set => audio_Compass.ChangeCourse(Heading.FromDegrees(value));
    }
    private readonly ObservableAsPropertyHelper<int> compassFilterRate;
    public int CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => audio_Compass.ChangeFilterRate(value);
    }
    private readonly ObservableAsPropertyHelper<CompassData> filteredCompass;
    public CompassData FilteredCompass => filteredCompass.Value;
    private readonly ObservableAsPropertyHelper<string> headingText;
    public string HeadingText => headingText.Value;
    public ReactiveCommand<int, Unit> IncrementCourseCommand { get; }
}

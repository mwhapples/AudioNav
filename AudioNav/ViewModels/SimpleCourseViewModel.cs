using AudioNav.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class SimpleCourseViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly AudioCompass audioCompass;
    private readonly ObservableAsPropertyHelper<int> course;
    public SimpleCourseViewModel(AudioCompass audioCompass)
    {
        this.audioCompass = audioCompass;
        course = audioCompass.Course.Select(x => (int)x.Degrees).ToProperty(this, x => x.Course);
        IncrementCourseCommand = ReactiveCommand.Create<int>(x => Course += x);
    }
    public ViewModelActivator Activator { get; } = new();
    public int Course
    {
        get => course.Value;
        set => audioCompass.ChangeCourse(Heading.FromDegrees(value));
    }
    public ReactiveCommand<int, Unit> IncrementCourseCommand { get; }
}

using AudioNav.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace AudioNav.ViewModels;

public class CourseToHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    public CourseToHeadingViewModel(AudioCompass audioCompass)
    {
        heading = audioCompass.CompassHeading.Where(x => x is CompassData.HeadingReading).Select(x => (CompassData.HeadingReading)x).ToProperty(this, x => x.Heading);
        var canExecute = audioCompass.CompassHeading.Select(x => !(x is CompassData.HeadingReading));
        ChangeCourseToCurrentHeadingCommand = ReactiveCommand.Create(() => audioCompass.ChangeCourse(Heading.Heading));
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<CompassData.HeadingReading> heading;
    public CompassData.HeadingReading Heading => heading.Value;
    public ReactiveCommand<Unit, Unit> ChangeCourseToCurrentHeadingCommand { get; }
}

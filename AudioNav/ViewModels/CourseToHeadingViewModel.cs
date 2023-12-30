using AudioNav.Models;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class CourseToHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    public CourseToHeadingViewModel(AudioCompass audioCompass)
    {
        currentHeading = audioCompass.CompassHeading.Where(x => x is CompassData.HeadingReading).Select(x => ((CompassData.HeadingReading)x).Heading).ToProperty(this, x => x.CurrentHeading);
        var canExecute = audioCompass.CompassHeading.Select(x => x is CompassData.HeadingReading);
        ChangeCourseToCurrentHeadingCommand = ReactiveCommand.Create(() => audioCompass.ChangeCourse(CurrentHeading), canExecute);
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<Heading> currentHeading;
    public Heading CurrentHeading => currentHeading.Value;
    public ReactiveCommand<Unit, Unit> ChangeCourseToCurrentHeadingCommand { get; }
}

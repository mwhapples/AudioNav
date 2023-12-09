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
        var headingSubject = new BehaviorSubject<Heading>(Heading.FromDegrees(0));
        var canExecute = new BehaviorSubject<bool>(false);
        this.WhenActivated(disposables =>
        {
            audioCompass.CompassHeading.Where(x => x is CompassData.HeadingReading).Select(x => ((CompassData.HeadingReading)x).Heading).Subscribe(headingSubject).DisposeWith(disposables);
            audioCompass.CompassHeading.Select(x => !(x is CompassData.HeadingReading)).Subscribe(canExecute).DisposeWith(disposables);
        });
        currentHeading = headingSubject.ToProperty(this, x => x.CurrentHeading);
        ChangeCourseToCurrentHeadingCommand = ReactiveCommand.Create(() => audioCompass.ChangeCourse(CurrentHeading));
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<Heading> currentHeading;
    public Heading CurrentHeading => currentHeading.Value;
    public ReactiveCommand<Unit, Unit> ChangeCourseToCurrentHeadingCommand { get; }
}

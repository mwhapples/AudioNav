using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class SimpleHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    public SimpleHeadingViewModel(AudioCompass audioCompass)
    {
        var headingSubject = new BehaviorSubject<string>("");
        this.WhenActivated(disposables =>
        {
            audioCompass.CompassHeading.Select(x => x switch
            {
                CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
                _ => "---"
            }).Subscribe(headingSubject).DisposeWith(disposables);
        });
        headingText = headingSubject.ToProperty(this, x => x.HeadingText);
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<string> headingText;
    public string HeadingText => headingText.Value;
}

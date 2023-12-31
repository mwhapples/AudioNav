using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class SimpleHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ObservableAsPropertyHelper<string> headingText;
    public SimpleHeadingViewModel(AudioCompass audioCompass)
    {
        headingText = audioCompass.CompassHeading.Select(x => x switch
        {
            CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
            _ => "---"
        }).ToProperty(this, x => x.HeadingText);
    }
    public ViewModelActivator Activator { get; } = new();
    public string HeadingText => headingText.Value;
}

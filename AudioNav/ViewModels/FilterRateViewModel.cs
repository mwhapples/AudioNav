using AudioNav.Models;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace AudioNav.ViewModels;

public class FilterRateViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly AudioCompass audioCompass;
    public FilterRateViewModel(AudioCompass audioCompass)
    {
        this.audioCompass = audioCompass;
        compassFilterRate = audioCompass.FilterRate.ToProperty(this, x => x.CompassFilterRate);
        this.WhenActivated(disposables =>
        {
            compassFilterRate.DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();
    private readonly ObservableAsPropertyHelper<int> compassFilterRate;
    public int CompassFilterRate
    {
        get => compassFilterRate.Value;
        set => audioCompass.ChangeFilterRate(value);
    }
}

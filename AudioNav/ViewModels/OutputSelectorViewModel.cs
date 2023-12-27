using AudioNav.Models;
using ReactiveUI;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace AudioNav.ViewModels;

public class OutputSelectorViewModel : ReactiveObject, IActivatableViewModel
{
    private AudioCompass audioCompass;
    private ObservableAsPropertyHelper<IAudioCompassOutput> output;
    public OutputSelectorViewModel(AudioCompass audioCompass)
    {
        this.audioCompass = audioCompass;
        output = audioCompass.Output.ToProperty(this, x => x.Output);
        this.WhenActivated(disposables =>
        {
            output.DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();
    public ImmutableList<IAudioCompassOutput> AvailableOutputs => audioCompass.AvailableOutputs;
    public IAudioCompassOutput Output
    {
        get => output.Value;
        set => audioCompass.ChangeOutput(value);
    }
}

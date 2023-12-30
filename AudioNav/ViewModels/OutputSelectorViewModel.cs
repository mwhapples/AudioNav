using AudioNav.Models;
using ReactiveUI;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace AudioNav.ViewModels;

public class OutputSelectorViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly AudioCompass audioCompass;
    private readonly ObservableAsPropertyHelper<IAudioCompassOutput> output;
    public OutputSelectorViewModel(AudioCompass audioCompass)
    {
        this.audioCompass = audioCompass;
        output = audioCompass.Output.ToProperty(this, x => x.Output);
    }
    public ViewModelActivator Activator { get; } = new();
    public ImmutableList<IAudioCompassOutput> AvailableOutputs => audioCompass.AvailableOutputs;
    public IAudioCompassOutput Output
    {
        get => output.Value;
        set => audioCompass.ChangeOutput(value);
    }
}

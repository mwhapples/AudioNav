using AudioNav.Models;
using ReactiveUI;
using System;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace AudioNav.ViewModels;

public class SensorSelectorViewModel : ReactiveObject, IActivatableViewModel
{
    private AudioCompass audioCompass;
    private ObservableAsPropertyHelper<ICompassProvider> compassProvider;
    public SensorSelectorViewModel(AudioCompass audioCompass)
    {
        this.audioCompass = audioCompass;
        compassProvider = audioCompass.CompassProvider.ToProperty(this, x => x.CompassProvider);
        this.WhenActivated(disposables =>
        {
            compassProvider.DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();
    public ICompassProvider CompassProvider
    {
        get => compassProvider.Value;
        set => audioCompass.ChangeCompassProvider(value);
    }
    public ImmutableList<ICompassProvider> CompassProviders => audioCompass.AvailableSensors;
}

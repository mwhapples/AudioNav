using AudioNav.Models;
using Microsoft.Maui.Media;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace AudioNav.ViewModels;

public class SpeakAbsoluteHeadingViewModel : ReactiveObject, IActivatableViewModel
{
    public SpeakAbsoluteHeadingViewModel(AudioCompass audioCompass)
    {
        this.WhenActivated(disposables =>
        {
            var heading = new BehaviorSubject<CompassData>(new CompassData.NoHeading());
            audioCompass.CompassHeading.Subscribe(heading).DisposeWith(disposables);
            Observable.Create<Unit>(async (observer, cancellationToken) =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await TextToSpeech.Default.SpeakAsync(heading.Value switch
                        {
                            CompassData.HeadingReading r => r.Heading.Degrees.ToString("000"),
                            _ => "No heading"
                        }, cancelToken: cancellationToken);
                        await Task.Delay(TimeSpan.FromMilliseconds(500), cancellationToken:  cancellationToken);
                    }
                    catch (Exception) { }
                }
            }).Subscribe().DisposeWith(disposables);
        });
    }
    public ViewModelActivator Activator { get; } = new();
}

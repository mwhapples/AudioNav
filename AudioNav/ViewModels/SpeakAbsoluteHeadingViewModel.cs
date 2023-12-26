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
    }
    public ViewModelActivator Activator { get; } = new();
}
